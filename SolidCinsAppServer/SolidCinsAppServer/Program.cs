using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using SolidCinsGUI.Packets;

class SolidCinsAppServer
{
    public static Dictionary<string, Client> clients = new Dictionary<string, Client>();
    private static List<OuterGateLogPacket> outerGateLogPackets = new List<OuterGateLogPacket>();
    private static byte[] data = new byte[1024];
    static Socket server = new Socket(AddressFamily.InterNetwork,
                                   SocketType.Stream,
                                   ProtocolType.Tcp);

    static ExchangeInfoPacket lastExchangeInfoPacket;
    static WeatherInfoPacket lastWeatherInfoPacket;

    private static bool isAcceptingSocket = false;

    public static void Main(string[] args)
    {
        //Fake Outer-Gate data (out-of scope), assumed it's csv file
        string[] lines = new string[] {"Fudayl,2342,2022-12-30T23:04:36.222Z",
                            "Fudayl,2342,2023-01-08T13:15:36.222Z",
                            "Mahmut,1238,2023-01-07T16:26:36.222Z",
                            "Fudayl,2342,2023-01-08T16:43:36.222Z",
                            "Mahmut,1238,2023-01-08T18:48:36.222Z",
                            "Fudayl,2342,2023-01-08T21:14:36.222Z",
                            "Fudayl,2342,2023-01-08T23:20:36.222Z" };
        foreach (string line in lines)
        {
            string[] lineParts = line.Split(",");
            OuterGateLogPacket outerGateLogPacket = new OuterGateLogPacket();
            outerGateLogPacket.OwnerName = lineParts[0];
            outerGateLogPacket.CardId = lineParts[1];
            outerGateLogPacket.Time = lineParts[2];
            outerGateLogPackets.Add(outerGateLogPacket);
        }

        IPEndPoint iep = new IPEndPoint(IPAddress.Any, 8888);
        server.Bind(iep);
        server.Listen(8);
        GetExchangeRates();
        GetWeatherInfo();
        while (true)
        {
            if (!isAcceptingSocket)
            {
                isAcceptingSocket = true;
                server.AcceptAsync().ContinueWith((t) =>
                {
                    Socket newsocket = t.Result;
                    Client newClient = new Client(newsocket);
                    clients.Add(newsocket.RemoteEndPoint.ToString(), newClient);
                    newClient.InitiateReceive();
                    isAcceptingSocket = false;
                });
            }
            System.Threading.Thread.Sleep(500);
        }
    }

    private static void GetExchangeRates()
    {
        Task.Run(() =>
        {
            while (true)
            {
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("https://api.exchangerate-api.com/v4/latest/TRY");

                client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "")).ContinueWith((t) =>
                {
                    HttpResponseMessage response = t.Result;
                    response.Content.ReadAsStringAsync().ContinueWith((t2) =>
                    {
                        string result = t2.Result;
                        ExchangeRatesPacket packet = JsonSerializer.Deserialize<ExchangeRatesPacket>(result);
                        float Dollar = (float)packet.rates["USD"];
                        float Euro = (float)packet.rates["EUR"];
                        float Pound = (float)packet.rates["GBP"];
                        ExchangeInfoPacket exchangeInfoPacket = new ExchangeInfoPacket()
                        {
                            Dollar = (float)Math.Pow(Dollar, -1),
                            Euro = (float)Math.Pow(Euro, -1),
                            Pound = (float)Math.Pow(Pound, -1)
                        };
                        lastExchangeInfoPacket = exchangeInfoPacket;
                        Client.BroadcastEveryone(exchangeInfoPacket);
                    });
                });
                System.Threading.Thread.Sleep(10000);
            }
        });
    }

    private static void GetWeatherInfo()
    {
        Task.Run(() =>
        {
            while (true)
            {
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=38.41&longitude=27.13&current_weather=true&hourly=temperature_2m,relativehumidity_2m,windspeed_10m");

                client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "")).ContinueWith((t) =>
                {
                    HttpResponseMessage response = t.Result;
                    response.Content.ReadAsStringAsync().ContinueWith((t2) =>
                    {
                        string result = t2.Result;
                        WeatherResponsePacket packet = JsonSerializer.Deserialize<WeatherResponsePacket>(result);
                        float temperature = (float)packet.current_weather.temperature;
                        lastWeatherInfoPacket = new WeatherInfoPacket()
                        {
                            Temperature = temperature
                        };
                        Client.BroadcastEveryone(lastWeatherInfoPacket);
                    });
                });
                System.Threading.Thread.Sleep(10000);
            }
        });
    }



    public static void HandlePacket(Type messageType, object message, Client sender)
    {
        sender.reactivateListener();
        switch (messageType)
        {
            case var type when type == typeof(LoginPacket):
                LoginPacket loginPacket = (LoginPacket)message;
                string jsonData = File.ReadAllText("./Constants/Credentials.json");
                CredentialsPacket credentialsPacket = JsonSerializer.Deserialize<CredentialsPacket>(jsonData);
                string familyname = loginPacket.Familyname;
                string password = loginPacket.Password;
                if (credentialsPacket.credentials.ContainsKey(familyname) && password != "")
                {
                    string userPassword = credentialsPacket.credentials[familyname];
                    bool loggedIn = userPassword == password;
                    sender.Send(new LoginResultPacket() { Result = loggedIn, Familyname = familyname });
                }

                else
                {
                    sender.Send(new LoginResultPacket() { Result = false, Familyname = familyname });
                    System.Console.WriteLine("SENT FALSE");
                }
                break;
            case var type when type == typeof(ChatMessagePacket):
                ChatMessagePacket messagePacket = (ChatMessagePacket)message;
                Console.WriteLine(messagePacket.SenderFamilyName + ": " + messagePacket.Message);
                sender.Broadcast(messagePacket);
                break;
            case var type when type == typeof(ClientOnlinePacket):
                System.Console.WriteLine("Biri online oldu");
                ClientOnlinePacket onlinePacket = (ClientOnlinePacket)message;

                if (lastExchangeInfoPacket != null)
                {
                    sender.Send(lastExchangeInfoPacket);
                }
                //Sync current outerGateLogPackets with new client
                foreach (var log in outerGateLogPackets)
                {
                    sender.Send(log);
                }

                //Sync current online users with new client
                foreach (var client in clients)
                {
                    if (client.Value.isOnline)
                    {
                        sender.Send(new ClientOnlinePacket() { FamilyName = client.Value.FamilyName });
                    }
                }

                sender.FamilyName = onlinePacket.FamilyName;
                sender.isOnline = true;
                sender.Broadcast(onlinePacket);
                break;

            case var type when type == typeof(ClientOfflinePacket):
                ClientOfflinePacket offlinePacket = (ClientOfflinePacket)message;
                System.Console.WriteLine("Biri offline oldu");
                sender.isOnline = false;
                sender.Broadcast(offlinePacket).ContinueWith((t) =>
                {
                    clients.Remove(sender.GetSocket().RemoteEndPoint.ToString());
                });
                break;
        }
    }

}