using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

using GreenSmoke.Core;


// 사용될 아이템
namespace GreenSmoke.Core.Item
{
    // 몇개의 어댑터를 소유하는 멀티탭이다.
    [DataContract]
    public sealed class MultiStrip : ManagePhysicalParentConnectItem, IDisposable
    {
        private Group _CurrentGroup = Group.UnknowGroup();
        private int _MaxLength = 4;

        public MultiStrip(string id)
        {
            _ID = id;

            base._CurrentType = EnumItem.MultiStrip;
        }

        #region IDisposable 멤버

        public void Dispose()
        {
            //자신이 가입된 그룹에 삭제요청을 한다.
            _CurrentGroup.DisconnectedChild(this._ID);
        }

        #endregion
    }
}
