using System;
using System.Collections.Generic;

using GreenSmoke.Core.ClientService;
using GreenSmoke.Core.Item;
using GreenSmoke.Core.Policy;

// 로직
namespace GreenSmoke.Core.Logic
{
    public class ItemLogic : IItemInformation, IControlItem, IRegisterProduct, IConnectPhysicalItem,IManageItem
    {
        private Dictionary<string, ManagePhysicalItem> _PhysicalItem = new Dictionary<string, ManagePhysicalItem>();

        public Dictionary<string, ManagePhysicalItem> PhysicalItem
        {
            get { return _PhysicalItem; }
        }

        private Dictionary<string, Category> _Categories;

        public ItemLogic(Dictionary<string, Category> categories)
        {
            _Categories = categories;
        }

        #region IItemInformation 멤버

        public Dictionary<string, Group> GetGroupList()
        {
            Dictionary<string, Group> retDic = new Dictionary<string, Group>();
            foreach (string key in _PhysicalItem.Keys)
            {
                if (_PhysicalItem[key].CurrentType == EnumItem.Group) retDic.Add(_PhysicalItem[key].ID, _PhysicalItem[key] as Group);
            }
            return retDic;
        }

        public Dictionary<string, MultiStrip> GetMultiStripList()
        {
            Dictionary<string, MultiStrip> retDic = new Dictionary<string, MultiStrip>();
            foreach (string key in _PhysicalItem.Keys)
            {
                if (_PhysicalItem[key].CurrentType == EnumItem.MultiStrip) retDic.Add(_PhysicalItem[key].ID, _PhysicalItem[key] as MultiStrip);
            }
            return retDic;
        }

        public Dictionary<string, Manager> GetManagerList()
        {
            Dictionary<string, Manager> retDic = new Dictionary<string, Manager>();
            foreach (string key in _PhysicalItem.Keys)
            {
                if (_PhysicalItem[key].CurrentType == EnumItem.Manager) retDic.Add(_PhysicalItem[key].ID, _PhysicalItem[key] as Manager);
            }
            return retDic;
        }

        public Dictionary<string, Adaptor> GetAdaptorList()
        {
            Dictionary<string, Adaptor> retDic = new Dictionary<string, Adaptor>();
            foreach (string key in _PhysicalItem.Keys)
            {
                if (_PhysicalItem[key].CurrentType == EnumItem.Adaptor) retDic.Add(_PhysicalItem[key].ID, _PhysicalItem[key] as Adaptor);
            }
            return retDic;
        }

        public Dictionary<string, Category> GetCategoryList()
        {
            Dictionary<string, Category> retDic = new Dictionary<string, Category>();
            foreach (string key in _PhysicalItem.Keys)
            {
                if (_PhysicalItem[key].CurrentType == EnumItem.CategoryItem) retDic.Add(_PhysicalItem[key].ID, _PhysicalItem[key] as Category);
            }
            return retDic;
        }

        public Dictionary<string, Product> GetProductList()
        {
            Dictionary<string, Product> retDic = new Dictionary<string, Product>();
            foreach (string key in _PhysicalItem.Keys)
            {
                if (_PhysicalItem[key].CurrentType == EnumItem.Product) retDic.Add(_PhysicalItem[key].ID, _PhysicalItem[key] as Product);
            }
            return retDic;
        }

        #endregion

        #region IControlService 멤버

        public void ItemControl(EnumControl currentControl, string itemID)
        {
            ManagePhysicalItem tagetItem = PhysicalItem[itemID];

            switch (currentControl)
            {
                case EnumControl.PowerOn:
                    tagetItem.PowerOn();
                    break;
                case EnumControl.PowerOff:
                    tagetItem.PowerOff();
                    break;
                case EnumControl.Enable:
                    tagetItem.Enable();
                    break;
                case EnumControl.Disenable:
                    tagetItem.Disenable();
                    break;
                case EnumControl.Safe:
                    tagetItem.Safe();
                    break;
                case EnumControl.Unsafe:
                    tagetItem.Unsafe();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region IRegisterProduct 멤버

        public void RegisterProduct(string productID, string productImagePath, string name)
        {
            Product newProduct = new Product(productID,productImagePath,name);

            PhysicalItem.Add(productID, newProduct);
        }

        #endregion

        #region IManagePhysicalItem 멤버

        public void ConnectedAdaptor(string multiStripID, string adaptorID)
        {
            //TODO:객체생성
            ManagePhysicalItem thisAdaptor = null;
            ManagePhysicalParentConnectItem thisMultiStrip = PhysicalItem[multiStripID] as ManagePhysicalParentConnectItem;

            if (PhysicalItem.TryGetValue(adaptorID, out thisAdaptor))
            {
            }
            else
            {
                thisAdaptor = new Adaptor(adaptorID);
                PhysicalItem.Add(adaptorID, thisAdaptor);
            }
            
            thisAdaptor.Connected();
            thisMultiStrip.AddConnectedChild(thisAdaptor);
        }

        public void DisconnectedAdaptor(string multiStripID, string adaptorID)
        {
            ManagePhysicalParentConnectItem thisMultiStrip = PhysicalItem[multiStripID] as ManagePhysicalParentConnectItem;
            ManagePhysicalItem thisAdaptor = PhysicalItem[adaptorID];

            thisMultiStrip.DisconnectedChild(thisAdaptor.ID);
            thisAdaptor.Disconnected();
        }

        public void ConnectedMultiStrip(string multiStripID)
        {
            //TODO:객체생성
            ManagePhysicalItem thisMultiStrip = null;

            if (PhysicalItem.TryGetValue(multiStripID, out thisMultiStrip))
            {
            }
            else
            {
                thisMultiStrip = new MultiStrip(multiStripID);
                PhysicalItem.Add(multiStripID, thisMultiStrip);
            }

            thisMultiStrip.Connected();
        }

        public void DisconnectedMultiStrip(string multiStripID)
        {
            //TODO:객체제거
            ManagePhysicalItem thisMultiStrip = PhysicalItem[multiStripID];
            thisMultiStrip.Disconnected();
        }

        #endregion

        #region IManageItem 멤버

        public void CreateGroup(string name)
        {
            Group newGroup = new Group(name);

            PhysicalItem.Add(newGroup.ID, newGroup);
        }

        public void DeleteGroup(string groupID)
        {
            PhysicalItem.Remove(groupID);
        }

        public void CreateCategoryItem(string categoryID,string name,string ImagePath, EnumItem currentItem)
        {
            Category newItem = new Category(name, ImagePath, currentItem);

            _Categories.Add(categoryID, newItem);
        }

        public void DeleteCategoryItem(string categoryItemID)
        {
            _Categories.Remove(categoryItemID);
        }

        public void CreateManager(string managerID,string managerName, string password)
        {
            Manager newManager = new Manager(managerID, managerName, password);

            PhysicalItem.Add(newManager.ID, newManager);
        }

        public void DeleteManager(string managerID)
        {
            PhysicalItem.Remove(managerID);
        }

        public void AddCategoryItem(string parentItemID, string childItemID)
        {
            CategoryOnItem parentItem = PhysicalItem[parentItemID];
            Category category = _Categories[childItemID];

            parentItem.AddCategoryItem(category);
        }

        public void RemoveCategoryItem(string parentItemID, string childItemID)
        {
            CategoryOnItem parentItem = PhysicalItem[parentItemID];

            parentItem.RemoveCategoryItem(childItemID);
        }

        public void AddSubItem(string parentItemID, string subItemID)
        {
            UseSubItem parentItem = PhysicalItem[parentItemID] as UseSubItem;
            UseSubItem subItem = PhysicalItem[subItemID] as UseSubItem;

            subItem.ParentSubItem = parentItem;
            parentItem.AddSubItem(subItem);
        }

        public void RemoveSubItem(string parentItemID, string subItemID)
        {
            UseSubItem parentItem = PhysicalItem[parentItemID] as UseSubItem;
            UseSubItem subItem = PhysicalItem[subItemID] as UseSubItem;

            subItem.ParentSubItem = null;
            parentItem.RemoveSubItem(subItem);
        }

        public void AddManager(string itemID, string managerId)
        {
            Manager manager = _PhysicalItem[managerId] as Manager;
            _PhysicalItem[itemID].AddManager(manager);
        }

        public void RemoveManager(string itemID, string managerId)
        {
            _PhysicalItem[itemID].RemoveManager(managerId);
        }

        #endregion
    }
}
