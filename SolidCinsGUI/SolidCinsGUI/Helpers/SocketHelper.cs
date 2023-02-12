using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using SolidCinsGUI.Packets;

namespace SolidCinsGUI.Helpers
{
    public class SocketHelper
    {
        public static SocketHelper Instance { get; } = new SocketHelper();

        private Socket client;
        private static int size = 4096;
        private byte[] data = new byte[size];
        public string errorMsg = string.Empty;
        public bool isReceiving = false;
        public bool sendCompleted = false;
        int bytesReceived = 0;
        public byte[] buffer;

        private bool connected = false;

        public bool IsConnected()
        {
            return connected;
        }

        private SocketHelper()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    while (connected)
                    {
                        Thread.Sleep(50);
                        //Even a bit of delay is good enough to increase performance
                        if (!isReceiving)
                        {
                            //If one of the threads is currently waiting to receive not call ReceiveAsync again
                            Receive();
                        }
                    }
                }

            });
        }

        private void Receive()
        {
            isReceiving = true;
            buffer = new byte[size];

            client.ReceiveAsync(buffer, SocketFlags.None).ContinueWith(t =>
            {
                bytesReceived = t.Result;
                if (bytesReceived > 0)
                {
                    OnDataReceived(buffer, bytesReceived);
                }
                isReceiving = false;


            });
        }



        private void OnDataReceived(byte[] buffer, int bytesTransferred)
        {


            string stringData = Encoding.ASCII.GetString(buffer, 0, bytesTransferred);
            string[] packets = stringData.Split('\0');
            for (int i = 0; i < packets.Length; i++)
            {
                if (packets[i] == "")
                {
                    continue;
                }
                string[] dataSections = packets[i].Split("&_FM_&");
                if (dataSections.Length != 2)
                {
                    Debug.WriteLine("Unknown data received");
                    return;
                }
                Type messageType = Type.GetType(dataSections[0]);


                string deserializableSection = dataSections[1].TrimEnd('\0');

                HandlePacket(messageType, JsonSerializer.Deserialize(deserializableSection, messageType));
            }
        }

        private void HandlePacket(Type messageType, object message)
        {
            switch (messageType)
            {
                case var type when type == typeof(ChatMessagePacket):
                    ChatMessagePacket messagePacket = (ChatMessagePacket)message;
                    ThreadHelper.AddItem(
                        ClientPanelForm.Instance,
                        ClientPanelForm.Instance.chatListBox,
                        $"{messagePacket.SenderFamilyName}: {messagePacket.Message}"
                    );
                    break;

                case var type when type == typeof(LoginResultPacket):
                    LoginResultPacket resultPacket = (LoginResultPacket)message;
                    bool successfullyLoggedIn = resultPacket.Result;
                    string familyname = resultPacket.Familyname;
                    if (successfullyLoggedIn)
                    {

                        ThreadHelper.HideForm(LoginForm.Instance);
                        ThreadHelper.ShowForm(ClientPanelForm.Instance);
                        Instance.Send(new ClientOnlinePacket { FamilyName = familyname });
                    }
                    else
                    {
                        MessageBox.Show("Credentials are Wrong!");
                    }
                    break;
                case var type when type == typeof(ClientOnlinePacket):
                    ClientOnlinePacket onlinePacket = (ClientOnlinePacket)message;
                    ThreadHelper.AddItem(
                        ClientPanelForm.Instance,
                        ClientPanelForm.Instance.onlineClientListBox,
                        onlinePacket.FamilyName
                    );
                    break;
                case var type when type == typeof(ClientOfflinePacket):
                    ClientOfflinePacket offlinePacket = (ClientOfflinePacket)message;
                    ThreadHelper.RemoveItem(
                        ClientPanelForm.Instance,
                        ClientPanelForm.Instance.onlineClientListBox,
                        offlinePacket.FamilyName);
                    break;
                case var type when type == typeof(ExchangeInfoPacket):
                    ExchangeInfoPacket exchangeInfo = (ExchangeInfoPacket)message;
                    ThreadHelper.SetText(ClientPanelForm.Instance, ClientPanelForm.Instance.dollarLabel, exchangeInfo.Dollar.ToString("0.0000"));
                    ThreadHelper.SetText(ClientPanelForm.Instance, ClientPanelForm.Instance.euroLabel, exchangeInfo.Euro.ToString("0.0000"));
                    ThreadHelper.SetText(ClientPanelForm.Instance, ClientPanelForm.Instance.poundLabel, exchangeInfo.Pound.ToString("0.0000"));
                    break;
                case var type when type == typeof(WeatherInfoPacket):
                    WeatherInfoPacket weatherInfo = (WeatherInfoPacket)message;
                    ThreadHelper.SetText(ClientPanelForm.Instance, ClientPanelForm.Instance.weatherLabel, weatherInfo.Temperature.ToString("0.0"));
                    break;
                case var type when type == typeof(OuterGateLogPacket):
                    OuterGateLogPacket outerGateLog = (OuterGateLogPacket)message;
                    ThreadHelper.AddItem(
                        ClientPanelForm.Instance,
                        ClientPanelForm.Instance.gateLogListBox,
                        $"{DateTimeOffset.Parse(outerGateLog.Time).ToString("dd/MM/yyyy HH:mm:ss")} - {outerGateLog.OwnerName}#{outerGateLog.CardId}"
                    );
                    break;

            }
        }



        async public Task<bool> Connect(string ip, int port)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                await client.ConnectAsync(ip, port);
                connected = true;
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("===CONNECTION-ERRROR===");
                return false;
            }
        }

        public Task Send(string message)
        {
            return client.SendAsync(Encoding.UTF8.GetBytes(message), SocketFlags.None);
        }

        public void Send(object message)
        {
            Type messageType = Type.GetType(message.GetType().ToString());
            if (messageType == null)
            {
                return;
            }

            string dataHead = messageType.FullName + "&_FM_&";
            string dataBody = JsonSerializer.Serialize(message);

            client.SendAsync(
                Encoding.UTF8.GetBytes(dataHead + dataBody),
                SocketFlags.None);
        }

        public void Close()
        {
            client.Close();
        }


    }
}
