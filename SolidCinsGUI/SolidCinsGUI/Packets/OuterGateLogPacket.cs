using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidCinsGUI.Packets
{
    public class OuterGateLogPacket
    {
        public string CardId { get; set; }
        public string OwnerName { get; set; }
        public string Time { get; set; }
    }
}
