using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using SolidCinsGUI.Packets;

public class Client
{
    private bool isReceiving = false;
    private Socket clientSocket;
    public bool isOnline;

    public string FamilyName { get; set; }

    public Client(Socket clientSocket)
    {
        this.clientSocket = clientSocket;
    }

    public Socket GetSocket()
    {
        return clientSocket;
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

        clientSocket.SendAsync(
            Encoding.UTF8.GetBytes(dataHead + dataBody + "\0"),
            SocketFlags.None);
    }

    public void Receive()
    {
        byte[] buffer = new byte[4096];
        clientSocket.ReceiveAsync(buffer, SocketFlags.None).ContinueWith((t) =>
        {
            string stringData = Encoding.ASCII.GetString(buffer);
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
                    Console.WriteLine("Unknown data received");
                    return;
                }
                Type messageType = Type.GetType(dataSections[0]);

                string deserializableSection = dataSections[1].TrimEnd('\0');
                SolidCinsAppServer.HandlePacket(messageType, JsonSerializer.Deserialize(deserializableSection, messageType), this);
            }
        });
    }

    
    public void reactivateListener()
    {
        //this function is called when the received data
        //is handled by SolidCinsAppServer.HandlePacket
        isReceiving = false;
    }

    public void InitiateReceive()
    {
        Task.Run(() =>
        {
            while (true)
            {
                //Delay to prevent spamming and to use less CPU
                System.Threading.Thread.Sleep(1000);
                if (!isReceiving)
                {
                    //Only one receive can be active at a time
                    isReceiving = true;
                    Receive();
                }
            }
        });
    }

    public Task Broadcast(object message)
    {
        return Task.Run(() =>
        {
            foreach (Client client in SolidCinsAppServer.clients.Values)
            {
                if (client != this)
                {
                    System.Console.WriteLine("Sending to " + client.FamilyName);
                    client.Send(message);
                }
            }
        });
    }

    public static void BroadcastEveryone(object message)
    {
        foreach (Client client in SolidCinsAppServer.clients.Values)
        {
            if (client.isOnline)
            {
                client.Send(message);
            }
        }
    }
}