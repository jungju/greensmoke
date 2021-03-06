using System;
using System.Collections.Generic;

using GreenSmoke.Core;


// 정책사항
namespace GreenSmoke.Core.Policy
{
    // 카테고리별에 대한 감시를 하고 정의된 객체와 현재값이 같으면 반응을 한다.
    public sealed class Supervision : UseChildPolicy, IDisposable
    {
        private static int __Seq = 0;

        public Supervision(string name)
        {
            _Name = name;

            __Seq++;
            _ID = _ID = Config.GlobalDefine.SupervisionInitial.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day + __Seq.ToString();
        }

        #region IDisposable 멤버

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
