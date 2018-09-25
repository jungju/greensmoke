using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

using GreenSmoke.Core.Item;
using GreenSmoke.Core.Policy;

#region 전체적인 인터페이스

namespace GreenSmoke.Core
{
    // 자식을 관리하는 행동이다.
    public interface IControlParentItem
    {
        // 하위 객체를 추가한다
        void AddChild(GreenSmokeItem child);
        void RemoveChild(string childID);
        GreenSmokeItem SearchChild(string childID);
        int Length{get;}
    }

    public interface ICategoryItem
    {
        void AddCategoryItem(Category categoryItem);
        void RemoveCategoryItem(string categoryItemID);
        Category IsIncludeCategoryItem(string categoryItemID);
    }

    public interface IControlManager
    {
        Manager IsIncludeManager(string managerID);
        void AddManager(Manager manager);
        void RemoveManager(string managerID);
    }

    public interface IControlPhysicalItem
    {
        void Connected();
        void Disconnected();
        void Enable();
        void Disenable();
        void PowerOn();
        void PowerOff();
        void Safe();
        void Unsafe();
    }

    // 아이템에 그림이나 영상을 적용한다.
    public interface IMediaItem
    {

        string ImagePath{get;set;}
        string Name{get;set;}
    }
}

#endregion

#region 서비스용 인터페이스

namespace GreenSmoke.Core.ClientService
{
    [ServiceContract]
    public interface IAuthenticate
    {
        [OperationContract]
        bool Authenticator(string managerName, string password);
    }

    [ServiceContract]
    public interface IControlItem
    {
        [OperationContract]
        void ItemControl(EnumControl currentControl, string itemID);
    }

    [ServiceContract]
    public interface IItemInformation
    {
        [OperationContract]
        Dictionary<string, Group> GetGroupList();
        [OperationContract]
        Dictionary<string, MultiStrip> GetMultiStripList();
        [OperationContract]
        Dictionary<string, Manager> GetManagerList();
        [OperationContract]
        Dictionary<string, Adaptor> GetAdaptorList();
        [OperationContract]
        Dictionary<string, Category> GetCategoryList();
        [OperationContract]
        Dictionary<string, Product> GetProductList();
    }

    [ServiceContract]
    public interface IPolicyInformation
    {
        [OperationContract]
        Dictionary<string, UseChildPolicy> GetPolicyItem();
        [OperationContract]
        Dictionary<string, PolicyChild> GetPolicyChildrenItems(string policyID);
    }

    [ServiceContract]
    public interface IMonitorItem
    {
        [OperationContract]
        string GetAdaptorsCT();                 //#<ID>$<전류값>#<ID>$<전류값>
    }

    [ServiceContract]
    public interface IRegisterProduct
    {
        [OperationContract]
        void RegisterProduct(string productID, string strProductImagePath, string strName);
    }

    [ServiceContract]
    public interface IManagePolicy
    {
        [OperationContract]
        void CreatePolicy(string name, EnumPolicy kindPolicy);
        [OperationContract]
        void RemovePolicy(string policyID);
        [OperationContract]
        void AddScenarioItem(string scenarioID, string name, DateTime? offTime, DateTime? onTime, TimeSpan? pauseTimeSpan, TimeSpan? playTimeSpan, params string[] categoryItemsID);
        [OperationContract]
        void AddSupervisionItem(string supervisionID, string name, int criticalValue, TimeSpan runningSpan, EnumCTValueMode valueMode, EnumProcessMode processMode, params string[] categoryItemsID);
        [OperationContract]
        void RemovePolicyChildItem(string policyID, string policyChildItemID);
    }

    [ServiceContract]
    public interface IManageItem
    {
        [OperationContract]
        void CreateGroup(string name);
        [OperationContract]
        void DeleteGroup(string groupID);

        [OperationContract]
        void CreateCategoryItem(string categoryID, string name, string ImagePath, EnumItem currentItem);
        [OperationContract]
        void DeleteCategoryItem(string categoryItemID);

        [OperationContract]
        void CreateManager(string managerID, string managerName, string password);
        [OperationContract]
        void DeleteManager(string managerID);

        [OperationContract]
        void AddCategoryItem(string parentItemID, string childItemID);
        [OperationContract]
        void RemoveCategoryItem(string parentItemID, string childItemID);

        [OperationContract]
        void AddSubItem(string parentItemID, string subItemID);
        [OperationContract]
        void RemoveSubItem(string parentItemID, string subItemID);

        [OperationContract]
        void AddManager(string itemID,string managerId);
        [OperationContract]
        void RemoveManager(string itemID, string managerId);
    }
}

#endregion

#region 로직용 인터페이스

namespace GreenSmoke.Core.Logic
{
    // 물리적 아이템들의 관리에 대한 행동이다.
    public interface IConnectPhysicalItem
    {
        void ConnectedAdaptor(string multiStripID, string adaptorID);
        void DisconnectedAdaptor(string multiStrip, string adaptorID);
        void ConnectedMultiStrip(string itemID);
        void DisconnectedMultiStrip(string itemID);
    }
}

#endregion