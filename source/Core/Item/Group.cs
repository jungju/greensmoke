using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

using GreenSmoke.Core;
using GreenSmoke.Core.Policy;


// 사용될 아이템
namespace GreenSmoke.Core.Item
{
    // 몇개의 멀티탭을 포함하는 그룹이다.
    [DataContract]
    public sealed class Group : UseSubItem, IDisposable
    {
        private static int __Seq = 0;
        private static Group __UnknowGroup = new Group("UnknowGroup");

        public static Group UnknowGroup()
        {
            __UnknowGroup._ID = "UnknowGroup";

            return __UnknowGroup;
        }

        public Group(string name)
        {
            _Name = name;

            __Seq++;
            _ID = Config.GlobalDefine.GroupInitial.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day + __Seq.ToString();

            base._CurrentType = EnumItem.Group;
        }

        #region IDisposable 멤버

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}