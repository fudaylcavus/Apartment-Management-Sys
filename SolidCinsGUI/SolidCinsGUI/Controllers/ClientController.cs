using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SolidCinsGUI.Helpers;
using SolidCinsGUI.Packets;

namespace SolidCinsGUI.Controllers
{
    public static class ClientController
    {
        public static string? FamilyName { get; set; }

        public static bool SendLoginRequest(string familyname, string password)
        {
            try
            {
                SocketHelper.Instance.Send(new LoginPacket { Familyname = familyname, Password = password });
                return true;

            }
            catch
            {
                return false;
            }
        }

    }
}
