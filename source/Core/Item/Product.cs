using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

using GreenSmoke.Core.Policy;
// 사용될 아이템
namespace GreenSmoke.Core.Item
{
    // 전기를 사용하는 제품 클래스이다.
    [DataContract]
    public sealed class Product : ManagePhysicalItem, IDisposable
    {
        private static int __Seq = 0;
        private static Product __UnknowProduct = new Product("UnknowProduct", Config.Path.UnknowProductImagePath, "UnknowProduct");

        public static Product UnknowProduct
        {
            get { return Product.__UnknowProduct; }
            set { Product.__UnknowProduct = value; }
        }


        public Product(string id, string productImagePath,string name)
        {
            __Seq++;

            _ID = id;
            _ImagePath = productImagePath;
            _Name = name;

            base._CurrentType = EnumItem.Product;
        }

        #region IDisposable 멤버

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
