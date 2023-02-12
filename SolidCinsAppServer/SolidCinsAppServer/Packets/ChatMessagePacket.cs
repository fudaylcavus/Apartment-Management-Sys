using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidCinsGUI.Packets
{
    public class ChatMessagePacket
    {
        public string SenderFamilyName { get; set; }
        public string Message { get; set; }
    }
}
