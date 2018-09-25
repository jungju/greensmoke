using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace GreenSmoke.Core.Communication
{
    public class TransmissionSerialPort
    {
        public delegate void ReceiveDataPort(object sender, EventArgs e);
        public event ReceiveDataPort OnReceiveDataPort;

        private SerialPort _ConnectedPort = new SerialPort(Config.InformationSerialPort.__PortName, Config.InformationSerialPort.__Baudrate, Config.InformationSerialPort.__Parity, Config.InformationSerialPort.__DataBit, Config.InformationSerialPort.__StopBits);

        public TransmissionSerialPort()
        {
            _ConnectedPort.DataReceived += new SerialDataReceivedEventHandler(_ConnectedPort_DataReceived);
        }

        public void PortOpen()
        {
            _ConnectedPort.Open();
        }

        public void PortClose()
        {
            _ConnectedPort.Close();
        }

        public void SendData(string data)
        {
            _ConnectedPort.WriteLine(data);
        }

        void _ConnectedPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string receiveData = _ConnectedPort.ReadExisting();

            OnReceiveDataPort(receiveData, EventArgs.Empty);
        }
    }
}