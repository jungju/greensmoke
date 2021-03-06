using System;
using System.Collections.Generic;

using GreenSmoke.Core.Communication;


// 로직
namespace GreenSmoke.Core.Logic
{
    public class CommunicationLogic
    {
        private TransmissionSerialPort _TransmissionSerialPort = new TransmissionSerialPort();

        private CoreLogic _CoreLogic;
        private Send _Sender;
        private Receive _Receiver;

        public CommunicationLogic(CoreLogic _logic)
        {
            _CoreLogic = _logic;
            _Sender = new Send(_TransmissionSerialPort);
            _Receiver = new Receive(_TransmissionSerialPort);

            _Receiver.MultiStripMessage += new Receive.MultiStripMessageHandler(_Receiver_MultiStripMessage);
        }

        void _Receiver_MultiStripMessage(object sender, ReceiveDataEvnetArg e)
        {
            switch (e.CurrentType)
            {
                case ReceiveDataEvnetArg.ReceiveType.Power:
                    break;
                case ReceiveDataEvnetArg.ReceiveType.CTValue:
                    break;
                case ReceiveDataEvnetArg.ReceiveType.ConnectedMultiStrip:
                    _CoreLogic.ConnectedMultiStrip(e.MultiStripID);
                    break;
                case ReceiveDataEvnetArg.ReceiveType.ConnectedAdaptor:
                    _CoreLogic.ConnectedAdaptor(e.MultiStripID, e.AdaptorID);
                    break;
                case ReceiveDataEvnetArg.ReceiveType.DisconnectedAdaptor:
                    _CoreLogic.DisconnectedAdaptor(e.MultiStripID,e.AdaptorID);
                    break;
                default:
                    break;
            }
        }
    }
}
