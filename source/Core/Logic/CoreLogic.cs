using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

using GreenSmoke.Core.Item;
using GreenSmoke.Core.Policy;
using GreenSmoke.Core.ClientService;

using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace GreenSmoke.Core.Logic
{
    // 서비스에 대한 모든 로직이 구현되어 있다.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CoreLogic : ILocal,IMobile,IItemInformation, IPolicyInformation, IAuthenticate, IControlItem, IManagePolicy, IRegisterProduct, IMonitorItem, IConnectPhysicalItem, IManageItem
    {
        private Dictionary<string, Category> _Categories;
        private CommunicationLogic _CommunicationLogic;
        private ItemLogic _ItemLogic;
        private PolicyLogic _PolicyLogic;

        SqlDatabase _Database = new SqlDatabase(Config.InformationDatabase.__ConnectionString);

        public CoreLogic()
        {
            _Categories = new Dictionary<string, Category>();
            _CommunicationLogic = new CommunicationLogic(this);
            _ItemLogic = new ItemLogic(_Categories);
            _PolicyLogic = new PolicyLogic(_Categories);

            TestData();
        }

        public void ServiceOn()
        {
            try
            {
                ServiceHost host = new ServiceHost(this);
                host.Open();
            }
            catch (Exception ex)
            {
                
            }
        }

        public string TestService(string clientMessase)
        {
            return clientMessase + "good";
        }

        #region Test데이터

        void TestData()
        {
            _ItemLogic.CreateGroup("기본1");

            _ItemLogic.CreateManager("사람1","사람1","1234");
            _ItemLogic.CreateManager("사람2", "사람2", "1234");
            _ItemLogic.CreateManager("사람3", "사람3", "1234");

            _ItemLogic.CreateCategoryItem("C01","밥솥", "", EnumItem.CategoryItem);
            _ItemLogic.CreateCategoryItem("C02", "냉장고", "", EnumItem.CategoryItem);
            _ItemLogic.CreateCategoryItem("C03", "세탁기", "", EnumItem.CategoryItem);
            _ItemLogic.CreateCategoryItem("C04", "TV", "", EnumItem.CategoryItem);

            _ItemLogic.ConnectedMultiStrip("M0001");
            _ItemLogic.ConnectedAdaptor("M0001", "A0001");
            _ItemLogic.ConnectedAdaptor("M0001", "A0002");
            _ItemLogic.ConnectedAdaptor("M0001", "A0003");
            _ItemLogic.ConnectedAdaptor("M0001", "A0004");

            _ItemLogic.ConnectedMultiStrip("M0002");
            _ItemLogic.ConnectedAdaptor("M0002", "A0005");
            _ItemLogic.ConnectedAdaptor("M0002", "A0006");
            _ItemLogic.ConnectedAdaptor("M0002", "A0007");
            _ItemLogic.ConnectedAdaptor("M0002", "A0008");

        }

        #endregion

        #region 데이터베이스 로드

        public void DataLoad()
        {
            DataSet allItems = _Database.ExecuteDataSet(Config.InformationDatabase.__SP_AllItemLoadProcdure);
            DataSet allPolicies = _Database.ExecuteDataSet(Config.InformationDatabase.__SP_AllPolicyLoadProcdure);

            foreach (DataRow row in allItems.Tables[0].Rows)
            {
                string itemID = GetStringType("ItemID", row);
                string name = GetStringType("Name", row);
                string imagePath = GetStringType("ImagePath", row);
                DateTime connectedTime = GetDateTimeType("ConnectedTime", row);
                DateTime disconnectedTime = GetDateTimeType("DisconnectedTime", row);
                bool isEnable = GetBoolType("IsEnable", row);
                bool isPower = GetBoolType("IsPower", row);
                bool isSafe = GetBoolType("IsSafe", row);
                EnumPowerStateMode powerStateMode = (EnumPowerStateMode)Enum.Parse(typeof(EnumPowerStateMode), GetStringType("AD_PowerStateMode", row));
                string itemType = GetStringType("ItemType", row);

                ManagePhysicalItem item = new ManagePhysicalItem();
                item.ID = itemID;
                item.ImagePath = imagePath;
                item.ConnectedTime = connectedTime;
                item.DisconnectedTime = disconnectedTime;
                item.IsEnable = isEnable;
                item.IsPowerOn = isPower;
                item.IsSafe = isSafe;

                if (itemType.Equals("Adaptor"))
                {
                    Adaptor adaptor = (Adaptor)item;
                    adaptor.CurrentPowerStateMode = powerStateMode;
                }
                _ItemLogic.PhysicalItem.Add(item.ID, item);

                DataSet allItemChild = _Database.ExecuteDataSet(Config.InformationDatabase.__SP_AllItemLoadProcdure, itemID);
                foreach (DataRow cRow in allItemChild.Tables[0].Rows)
                {
                    string cItemID = GetStringType("ChildItemID", cRow);
                    EnumChildType childType = (EnumChildType)Enum.Parse(typeof(EnumChildType), GetStringType("ChildTypeID", cRow));

                    switch (childType)
                    {
                        case EnumChildType.GroupInGroup:
                        case EnumChildType.ManagerInManager:
                        case EnumChildType.CategoryInCategory:
                            _ItemLogic.AddSubItem(itemID, cItemID);
                            break;
                        case EnumChildType.GroupInManager:
                            _ItemLogic.AddManager(itemID, cItemID);
                            break;
                        case EnumChildType.CategoryInItem:
                            _ItemLogic.AddCategoryItem(itemID, cItemID);
                            break;
                        default:
                            break;
                    }
                }
            }

            foreach (DataRow row in allPolicies.Tables[0].Rows)
            {
                string policyID = GetStringType("PolicyID", row);
                string policyType = GetStringType("PolicyType", row);
                string policyName = GetStringType("Name", row);

                DataSet allPoliciesCategory = _Database.ExecuteDataSet(Config.InformationDatabase.__SP_AllItemLoadProcdure,policyID);
                string[] categoryItemsID = new string[allPoliciesCategory.Tables[0].Rows.Count];
                int index = 0;
                foreach (DataRow cRow in allPoliciesCategory.Tables[0].Rows)
                {
                    categoryItemsID[index] = GetStringType("ChildItemID", cRow);
                    index++;
                }

                switch (policyType)
                {
                    case "scenario":
                        DateTime onTime = GetDateTimeType("SC_OnTime", row);
                        DateTime offTime = GetDateTimeType("SC_OffTime", row);
                        TimeSpan pauseTimeSpan = GetTimeSpan("SC_PauseTimeSpan", row);
                        TimeSpan playTimeSpan = GetTimeSpan("SC_PlayTimeSpan", row);
                        EnumControlTimeMode controlTimeMode = (EnumControlTimeMode)Enum.Parse(typeof(EnumControlTimeMode), GetStringType("SC_ControlTimeMode", row));
                        EnumControlSpanMode controlSpanMode = (EnumControlSpanMode)Enum.Parse(typeof(EnumControlSpanMode), GetStringType("SC_ControlSpanMode", row));

                        _PolicyLogic.AddScenarioItem(policyID, policyName, offTime, onTime, pauseTimeSpan, playTimeSpan, categoryItemsID);

                        break;
                    case "supervision":
                        int criticalValue = GetIntType("SU_CTCriticalValue", row);
                        TimeSpan runningSpan = GetTimeSpan("SU_RunningSpan", row);
                        EnumCTValueMode ctValuMode = (EnumCTValueMode)Enum.Parse(typeof(EnumCTValueMode), GetStringType("SU_CTValueMode", row));
                        EnumProcessMode processMode = (EnumProcessMode)Enum.Parse(typeof(EnumProcessMode), GetStringType("SU_ProcessMode", row));
                        EnumPowerStateMode powerStateMode = (EnumPowerStateMode)Enum.Parse(typeof(EnumPowerStateMode), GetStringType("SU_PowerStateMode", row));

                        _PolicyLogic.AddSupervisionItem(policyID, policyName, criticalValue, runningSpan, ctValuMode, processMode, categoryItemsID);

                        break;
                }
            }
        }

        public int GetIntType(string att, DataRow row)
        {

            string ret = row[att].ToString();
            int finRet=0;
            if (string.IsNullOrEmpty(ret))
            {
                finRet = int.Parse(ret);
            }
            return finRet;
        }

        public string GetStringType(string att, DataRow row)
        {
            string ret = row[att].ToString();
            return ret;
        }
        public DateTime GetDateTimeType(string att, DataRow row)
        {
            string ret = row[att].ToString();
            DateTime finRet = DateTime.Now;
            if (string.IsNullOrEmpty(ret))
            {
                finRet = DateTime.Parse(ret);
            }
            return finRet;
        }

        public bool GetBoolType(string att, DataRow row)
        {
            string ret = row[att].ToString();
            bool finRet = false;
            if (string.IsNullOrEmpty(ret))
            {
                finRet = bool.Parse(ret);
            }
            return finRet;
        }

        public TimeSpan GetTimeSpan(string att, DataRow row)
        {
            string ret = row[att].ToString();
            TimeSpan finRet = TimeSpan.MaxValue;
            if (string.IsNullOrEmpty(ret))
            {
                finRet = TimeSpan.FromMinutes(double.Parse(ret));
            }
            return finRet;
        }

        #endregion

        #region IItemInformation 멤버

        public Dictionary<string, Group> GetGroupList()
        {
            return _ItemLogic.GetGroupList();
        }

        public Dictionary<string, MultiStrip> GetMultiStripList()
        {
            return _ItemLogic.GetMultiStripList();
        }

        public Dictionary<string, Manager> GetManagerList()
        {
            return _ItemLogic.GetManagerList();
        }

        public Dictionary<string, Adaptor> GetAdaptorList()
        {
            return _ItemLogic.GetAdaptorList();
        }

        public Dictionary<string, Category> GetCategoryList()
        {
            return _ItemLogic.GetCategoryList();
        }

        public Dictionary<string, Product> GetProductList()
        {
            return _ItemLogic.GetProductList();
        }

        #endregion

        #region IPolicyInformation 멤버

        public Dictionary<string, UseChildPolicy> GetPolicyItem()
        {
            return _PolicyLogic.Policies;
        }
        public Dictionary<string, PolicyChild> GetPolicyChildrenItems(string policyID)
        {
            return _PolicyLogic.GetPolicyChildrenItems(policyID);
        }

        #endregion

        #region IManagePolicy 멤버

        public void CreatePolicy(string name,EnumPolicy kindPolicy)
        {
            _PolicyLogic.CreatePolicy(name, kindPolicy);
        }

        public void RemovePolicy(string PolicyID)
        {
            _PolicyLogic.RemovePolicy(PolicyID);
        }

        public void AddScenarioItem(string scenarioID, string name, DateTime? offTime, DateTime? onTime, TimeSpan? pauseTimeSpan, TimeSpan? playTimeSpan, params string[] categoryItemsID)
        {
            _PolicyLogic.AddScenarioItem(scenarioID, name, offTime, onTime, pauseTimeSpan, playTimeSpan, categoryItemsID);
        }

        public void AddSupervisionItem(string supervisionID, string name, int criticalValue, TimeSpan runningSpan, EnumCTValueMode valueMode, EnumProcessMode processMode, params string[] categoryItemsID)
        {
            _PolicyLogic.AddSupervisionItem(supervisionID, name, criticalValue, runningSpan, valueMode, processMode, categoryItemsID);
        }

        public void RemovePolicyChildItem(string policyID, string policyChildItemID)
        {
            _PolicyLogic.RemovePolicyChildItem(policyID, policyChildItemID);
        }
        #endregion

        #region IRegisterProduct 멤버

        public void RegisterProduct(string productID, string productImagePath, string name)
        {
            _ItemLogic.RegisterProduct(productID, productImagePath, name);
        }

        #endregion

        #region IMonitorService 멤버

        public string GetAdaptorsCT()
        {
            //TODO:전류정보를 보내준다.
            throw new System.NotImplementedException();
        }

        #endregion

        #region IManagePhysicalItem 멤버

        public void ConnectedAdaptor(string multiStrip, string adaptorID)
        {
            _ItemLogic.ConnectedAdaptor(multiStrip, adaptorID);
        }

        public void DisconnectedAdaptor(string multiStrip,string adaptorID)
        {
            _ItemLogic.DisconnectedAdaptor(multiStrip,adaptorID);
        }

        public void ConnectedMultiStrip(string multiStrip)
        {
            _ItemLogic.ConnectedMultiStrip(multiStrip);
        }

        public void DisconnectedMultiStrip(string multiStrip)
        {
            _ItemLogic.DisconnectedMultiStrip(multiStrip);
        }

        #endregion

        #region IControlService 멤버

        public void ItemControl(EnumControl currentControl, string itemID)
        {
            _ItemLogic.ItemControl(currentControl, itemID);
        }

        #endregion

        #region IAuthenticate 멤버

        public bool Authenticator(string managerName, string password)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IManageItem 멤버

        public void CreateGroup(string name)
        {
            _ItemLogic.CreateGroup(name);
        }

        public void DeleteGroup(string groupID)
        {
            _ItemLogic.DeleteGroup(groupID);
        }

        public void CreateCategoryItem(string categoryID, string name, string ImagePath, EnumItem currentItem)
        {
            _ItemLogic.CreateCategoryItem(categoryID,name, ImagePath, currentItem);
        }

        public void DeleteCategoryItem(string categoryItemID)
        {
            _ItemLogic.DeleteCategoryItem(categoryItemID);
        }

        public void CreateManager(string managerID,string managerName, string password)
        {
            _ItemLogic.CreateManager(managerID, managerName, password);
        }

        public void DeleteManager(string managerID)
        {
            _ItemLogic.DeleteManager(managerID);
        }

        public void AddCategoryItem(string parentItemID, string childItemID)
        {
            _ItemLogic.AddCategoryItem(parentItemID, childItemID);
        }

        public void RemoveCategoryItem(string parentItemID, string childItemID)
        {
            _ItemLogic.RemoveCategoryItem(parentItemID, childItemID);
        }

        public void AddSubItem(string parentItemID, string subItemID)
        {
            _ItemLogic.AddSubItem(parentItemID, subItemID);
        }

        public void RemoveSubItem(string parentItemID, string subItemID)
        {
            _ItemLogic.RemoveSubItem(parentItemID, subItemID);
        }

        public void AddManager(string itemID, string managerId)
        {
            _ItemLogic.AddManager(itemID, managerId);
        }

        public void RemoveManager(string itemID, string managerId)
        {
            _ItemLogic.RemoveManager(itemID, managerId);
        }

        #endregion

    }

}
