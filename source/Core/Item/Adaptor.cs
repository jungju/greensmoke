using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

// 사용될 아이템
namespace GreenSmoke.Core.Item
{
    // 하나의 제품과 연결되는 어답터이다.

    [DataContract]
    public sealed class Adaptor : ManagePhysicalParentConnectItem,IDisposable
    {
        private EnumPowerStateMode _CurrentPowerStateMode = EnumPowerStateMode.Unknow;
        private Product _ConnectedProduct = Product.UnknowProduct;

        [DataMember]
        public EnumPowerStateMode CurrentPowerStateMode{get { return _CurrentPowerStateMode; }set { _CurrentPowerStateMode = value; }}

        [DataMember]
        public Product ConnectedProduct { get { return _ConnectedProduct; } set { _ConnectedProduct = value; } }

        public Adaptor(string id)
            :base()
        {
            _ID = id;

            //_ConnectedTime = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second,DateTime.Now.Millisecond);

            base._CurrentType = EnumItem.Adaptor;
        }

        #region IDisposable 멤버

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}