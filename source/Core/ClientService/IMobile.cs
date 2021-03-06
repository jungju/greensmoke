using System.Runtime.Serialization;
using System.ServiceModel;

// 클라이언트에 대한 서비스들
namespace GreenSmoke.Core.ClientService
{

	// 전기를 관리 할 수 있는 모바일 전용 서비스이다.
    [ServiceContract]
    public interface IMobile : IItemInformation, IPolicyInformation,IAuthenticate, IControlItem, IRegisterProduct, IMonitorItem
	{

	}
}
