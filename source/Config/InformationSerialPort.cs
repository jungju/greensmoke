using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace GreenSmoke.Config
{
    public class InformationSerialPort
    {
        public const string __PortName = "COM1";
        public const int __Baudrate = 9600;
        public const Parity __Parity = Parity.Even;
        public const int __DataBit = 8;
        public const StopBits __StopBits = StopBits.One;
    }
}
