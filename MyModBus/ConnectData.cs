using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyModBus
{
    public class ConnectData
    {
        public string IP { get; set; }
        public int port { get; set; }
        public short startAddress { get; set; }
        public short numRegister { get; set; }

        public ConnectData(string IP, int port, short startAddress, short numRegister)
        {
            this.IP = IP;
            this.port = port;
            this.startAddress = startAddress;
            this.numRegister = numRegister;
        }
        public ConnectData()
        {
        }
    }
}
