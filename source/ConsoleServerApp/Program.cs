using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GreenSmoke.Core.Logic.CoreLogic core = new GreenSmoke.Core.Logic.CoreLogic();
            core.ServiceOn();

            Console.WriteLine("성공적으로 실행 완료");
            Console.ReadLine();
        }
    }
}
