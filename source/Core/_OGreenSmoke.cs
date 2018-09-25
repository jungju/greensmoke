using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

using GreenSmoke.Core.Item;
using GreenSmoke.Core.Policy;

namespace GreenSmoke.Core
{
    [DataContract]
    public class GreenSmokeObject
    {

    }

    [DataContract]
    public abstract class GreenSmokeItem : GreenSmokeObject
    {
        protected string _ID;

        [DataMember]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
    }

    [DataContract]
    public abstract class DefineItem : GreenSmokeItem, IMediaItem
    {
        protected string _Name;
        protected string _ImagePath;

        #region IMediaItem 멤버

        [DataMember]
        public string ImagePath { get { return _ImagePath; } set { _ImagePath = value; } }

        [DataMember]
        public string Name { get { return _Name; } set { _Name = value; } }

        #endregion
    }

    [DataContract]
    public abstract class CategoryOnItem : DefineItem, ICategoryItem
    {
        protected Dictionary<string, Category> _CategoryItems = new Dictionary<string, Category>();
        [DataMember]
        public Dictionary<string, Category> CategoryItems { get { return _CategoryItems; } set { _CategoryItems = value; } }

        #region ICategoryItem 멤버

        public void AddCategoryItem(Category categoryItem)
        {
            _CategoryItems.Add(categoryItem.ID, categoryItem);
        }

        public void RemoveCategoryItem(string categoryItemID)
        {
            _CategoryItems.Remove(categoryItemID);
        }

        public Category IsIncludeCategoryItem(string categoryItemID)
        {
            Category categoryItem = null;

            foreach (string key in _CategoryItems.Keys)
            {
                if (_CategoryItems.TryGetValue(categoryItemID, out categoryItem))
                    return categoryItem;
            }

            return null;
        }

        #endregion
    }

    [DataContract]
    public abstract class UseChildPolicy : DefineItem, IControlParentItem
    {
        private Dictionary<string, PolicyChild> _PolicyChildItem = new Dictionary<string, PolicyChild>();
        public Dictionary<string, PolicyChild> PolicyChildItems { get { return _PolicyChildItem; }}

        #region IControlParentItem 멤버

        public void AddChild(GreenSmokeItem child)
        {
            PolicyChildItems.Add(child.ID, child as PolicyChild);
        }

        public void RemoveChild(string childID)
        {
            PolicyChildItems.Remove(childID);
        }

        public GreenSmokeItem SearchChild(string childID)
        {
            return PolicyChildItems[childID] as GreenSmokeItem;
        }

        public int Length { get { return PolicyChildItems.Count; } }

        #endregion
    }

    [DataContract]
    public abstract class PolicyChild : CategoryOnItem
    {
    }

    [DataContract]
    public class ManagePhysicalItem : CategoryOnItem, IControlPhysicalItem, IControlManager
    {
        protected Dictionary<string, Manager> _Managers = new Dictionary<string,Manager>();
        protected bool _IsSafe = true;
        protected bool _IsEnable = true;
        protected bool _IsPowerOn = false;
        protected bool _IsConnected = false;
        protected DateTime _ConnectedTime;
        protected DateTime _DisconnectedTime;
        protected CategoryOnItem _ConnectedParentItem = null;
        protected EnumItem _CurrentType;

        public Dictionary<string, Item.Manager> Managers{get { return _Managers; }}
        [DataMember]
        public bool IsSafe { get { return _IsSafe; } set { _IsSafe = value; } }
        [DataMember]
        public bool IsEnable { get { return _IsEnable; } set { _IsEnable = value; } }
        [DataMember]
        public bool IsPowerOn { get { return _IsPowerOn; } set { _IsPowerOn = value; } }
        [DataMember]
        public bool IsConnected { get { return _IsConnected; } set { _IsConnected = value; } }
        [DataMember]
        public DateTime ConnectedTime { get { return _ConnectedTime; } set { _ConnectedTime = value; } }
        [DataMember]
        public DateTime DisconnectedTime { get { return _DisconnectedTime; } set { _DisconnectedTime = value; } }
        [DataMember]
        public EnumItem CurrentType { get { return _CurrentType; } set { _CurrentType = value; } }

        public CategoryOnItem ConnectedParentItem { get { return _ConnectedParentItem; } set { _ConnectedParentItem = value; } }

        #region IControlPhysicalItem 멤버

        public virtual void Connected()
        {
            _IsConnected = true;
            _ConnectedTime = DateTime.Now;
        }

        public virtual void Disconnected()
        {
            _IsConnected = false;
            _DisconnectedTime = DateTime.Now;
        }

        public virtual void Enable()
        {
            _IsEnable = true;
        }

        public virtual void Disenable()
        {
            _IsEnable = false;
        }

        public virtual void PowerOn()
        {
            _IsPowerOn = true;
        }

        public virtual void PowerOff()
        {
            _IsPowerOn = false;
        }

        public virtual void Safe()
        {
            _IsSafe = true;
        }

        public virtual void Unsafe()
        {
            _IsSafe = false;
        }

        #endregion

        #region IControlManager 멤버


        public void AddManager(Manager manager)
        {
            _Managers.Add(manager.ID, manager);
        }

        public void RemoveManager(string managerID)
        {
            _Managers.Remove(managerID);
        }

        public Manager IsIncludeManager(string managerID)
        {
            Manager manager = null;

            if (_Managers.TryGetValue(managerID, out manager))
                return manager;

            return null;
        }
        #endregion
    }

    [DataContract]
    public class ManagePhysicalParentConnectItem : ManagePhysicalItem
    {

        protected Dictionary<string, ManagePhysicalItem> _ConnectedChildItem = new Dictionary<string, ManagePhysicalItem>();
        public Dictionary<string, ManagePhysicalItem> ConnectedChildItem { get { return _ConnectedChildItem; } }

        public void AddConnectedChild(ManagePhysicalItem item)
        {
            item.ConnectedParentItem = this;
            ConnectedChildItem.Add(item.ID, item);
        }

        public void DisconnectedChild(string itemID)
        {
            ConnectedChildItem.Remove(itemID);
        }

        public Dictionary<string, ManagePhysicalItem> GetConnectedChildItem()
        {
            return _ConnectedChildItem;
        }
    }

    [DataContract]
    public class UseSubItem : ManagePhysicalParentConnectItem
    {
        protected UseSubItem _ParentSubItem = null;
        protected Dictionary<string, UseSubItem> _ChildSubItem = new Dictionary<string, UseSubItem>();

        [DataMember]
        public UseSubItem ParentSubItem { get { return _ParentSubItem; } set { _ParentSubItem = value; } }
        public Dictionary<string, UseSubItem> ChildSubItem{get { return _ChildSubItem; }}

        public void AddSubItem(UseSubItem item)
        {
            ChildSubItem.Add(item.ID,item);
        }

        public void RemoveSubItem(UseSubItem item)
        {
            ChildSubItem.Remove(item.ID);
        }
    }
}
