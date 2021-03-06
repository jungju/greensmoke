using System;
using System.Collections.Generic;

using GreenSmoke.Core;
using GreenSmoke.Core.Item;


// 정책사항
namespace GreenSmoke.Core.Policy
{
    // 감시에 사용되는 세부 아이템이다.
    public sealed class SupervisionItem : PolicyChild, IDisposable
    {
        private int _CTCriticalValue;
        private TimeSpan _RunningSpan;
        private EnumCTValueMode _CurrentCTValueMode;
        private EnumProcessMode _CurrentProcessMode;
        private EnumPowerStateMode _ChangePowerStateMode;

        public int CTCriticalValue{get { return _CTCriticalValue; }set { _CTCriticalValue = value; }}
        public TimeSpan RunningSpan{get { return _RunningSpan; }set { _RunningSpan = value; }}
        public EnumCTValueMode CurrentCTValueMode{get { return _CurrentCTValueMode; }set { _CurrentCTValueMode = value; }}
        public EnumProcessMode CurrentProcessMode{get { return _CurrentProcessMode; }set { _CurrentProcessMode = value; }}
        public EnumPowerStateMode ChangePowerStateMode{get { return _ChangePowerStateMode; }set { _ChangePowerStateMode = value; }}

        public SupervisionItem(string name)
        {
            _Name = name;
        }

        #region IDisposable 멤버

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
