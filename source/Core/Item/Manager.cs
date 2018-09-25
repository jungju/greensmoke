using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

using GreenSmoke.Core.Policy;

namespace GreenSmoke.Core.Item
{
    [DataContract]
    public sealed class Manager : UseSubItem, IDisposable
    {
        private static int __Seq = 0;

        private string _Password;

        public Manager(string id, string name, string password)
        {
            __Seq++;

            _ID = id;
            _Name = name;
            _Password = password;

            base._CurrentType = EnumItem.Manager;
        }

        #region IDisposable 멤버

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
