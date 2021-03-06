using GreenSmoke.Core;


// 통신
namespace GreenSmoke.Core.Communication
{
	// 멀티탭에 메시지를 보내는 클래스이다.
	public class Send : GreenSmokeObject
	{
        private TransmissionSerialPort _TransmissionSerialPort;

        public Send(TransmissionSerialPort port)
        {
            _TransmissionSerialPort = port;
        }

        public void AdaptorPowerOn(string adaptorID)
        {
            string data = "#" + adaptorID + "$" + "1";

            _TransmissionSerialPort.SendData(data);
        }

        public void AdaptorPowerOff(string adaptorID)
        {
            string data = "#" + adaptorID + "$" + "0";

            _TransmissionSerialPort.SendData(data);
        }

        public void AdaptorCheck(string adaptorID)
        {
            string data = "#" + adaptorID + "$" + "CH";

            _TransmissionSerialPort.SendData(data);
        }
    }
}
