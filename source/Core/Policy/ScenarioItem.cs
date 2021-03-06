using System;
using System.Collections.Generic;

using GreenSmoke.Core;


// 정책사항
namespace GreenSmoke.Core.Policy
{
    // 시나리오에 사용되는 세부 아이템이다.
    public sealed class ScenarioItem : PolicyChild, IDisposable
    {
        private DateTime? _OffTime;
        private DateTime? _OnTime;
        private TimeSpan? _PauseTimeSpan;
        private TimeSpan? _PlayTimeSpan;

        private EnumControlTimeMode _CurrentTimeMode;
        private EnumControlSpanMode _CurrentSpanMode;

        public DateTime? OffTime { get { return _OffTime; } set { _OffTime = value; } }
        public DateTime? OnTime { get { return _OnTime; } set { _OnTime = value; } }
        public TimeSpan? PauseTimeSpan { get { return _PauseTimeSpan; } set { _PauseTimeSpan = value; } }
        public TimeSpan? PlayTimeSpan { get { return _PlayTimeSpan; } set { _PlayTimeSpan = value; } }
        public EnumControlTimeMode CurrentTimeMode { get { return _CurrentTimeMode; } set { _CurrentTimeMode = value; } }
        public EnumControlSpanMode CurrentSpanMode { get { return _CurrentSpanMode; } set { _CurrentSpanMode = value; } }

        public ScenarioItem(string name)
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
