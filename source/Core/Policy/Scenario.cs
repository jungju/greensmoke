using System;
using System.Collections.Generic;

using GreenSmoke.Core;


// 정책사항
namespace GreenSmoke.Core.Policy
{
    // 시나리오별로 제품을 관리한다.

    public sealed class Scenario : UseChildPolicy, IDisposable
    {
        private static int __Seq = 0;

        public Scenario(string name)
        {
            _Name = name;

            __Seq++;
            _ID = _ID = Config.GlobalDefine.ScenarioInitial.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day + __Seq.ToString();
        }

        #region IDisposable 멤버

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
