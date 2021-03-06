using System;
using System.Collections.Generic;

using GreenSmoke.Core;
using GreenSmoke.Config;

// 통신
namespace GreenSmoke.Core.Communication
{
	// 멀티탭에서 받은 메시지를 받는 클래스이다.
	public class Receive	: GreenSmokeObject
	{
        private TransmissionSerialPort _TransmissionSerialPort;

        public Receive(TransmissionSerialPort port)
        {
            _TransmissionSerialPort = port;

            _TransmissionSerialPort.OnReceiveDataPort += new TransmissionSerialPort.ReceiveDataPort(_TransmissionSerialPort_OnReceiveDataPort);
        }

        public delegate void MultiStripMessageHandler(object sender, ReceiveDataEvnetArg e);
        public event MultiStripMessageHandler MultiStripMessage;

        void _TransmissionSerialPort_OnReceiveDataPort(object sender, EventArgs e)
        {
            Paser(sender.ToString());
        }

		private void Paser(string data)
		{
            char[] splitPackageChar = { '#' };
            char[] splitValueChar = { '$' };

            string[] splitPackageDatas = data.Split(splitPackageChar);

            foreach (string packageData in splitPackageDatas)
            {
                string[] splitValueDatas  = packageData.Split(splitValueChar);

                string messageType = splitValueDatas[0];
                string adaptorID= "";
                string multiStripID = "";
                string value = "";

                ReceiveDataEvnetArg arg = new ReceiveDataEvnetArg();
                arg.AdaptorID = adaptorID;
                arg.MultiStripID = multiStripID;
                arg.Value = value;
                

                switch (messageType)
                {
                    case CommunicationValue.Power:
                        arg.CurrentType = ReceiveDataEvnetArg.ReceiveType.Power;
                        break;
                    case CommunicationValue.CTValue:
                        arg.CurrentType = ReceiveDataEvnetArg.ReceiveType.CTValue;
                        break;
                    case CommunicationValue.ConnectedMultiStrip:
                        arg.CurrentType = ReceiveDataEvnetArg.ReceiveType.ConnectedMultiStrip;
                        break;
                    case CommunicationValue.ConnectedAdaptor:
                        arg.CurrentType = ReceiveDataEvnetArg.ReceiveType.ConnectedAdaptor;
                        break;
                    case CommunicationValue.DisconnectedAdpator:
                        arg.CurrentType = ReceiveDataEvnetArg.ReceiveType.DisconnectedAdaptor;
                        break;
                }
                MultiStripMessage(_TransmissionSerialPort, arg);
            }
		}
	}

    public class ReceiveDataEvnetArg : EventArgs
    {
        public string MultiStripID;
        public string AdaptorID;
        public string Value;
        public ReceiveType CurrentType;

        public enum ReceiveType
        {
            Power,
            CTValue,
            ConnectedMultiStrip,
            ConnectedAdaptor,
            DisconnectedAdaptor,
            DisconnectedMultiStrip
        }
    }
}