using System;
using System.Runtime.Serialization;
using System.ServiceModel;

// 클라이언트에 대한 서비스들
namespace GreenSmoke.Core.ClientService
{
	//전기를 관리하는 지역에 대한 서비스이다.
    [ServiceContract]
    public interface ILocal : IItemInformation, IPolicyInformation,IAuthenticate, IControlItem, IManagePolicy, IRegisterProduct, IMonitorItem, IManageItem
    {
        [OperationContract]
        string TestService(string clientMessase);
    }
}
