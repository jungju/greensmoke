using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenSmoke.Config
{
    public class GlobalDefine
    {
        public const char MultiStripInitial = 'M';
        public const char AdaptorInitial = 'A';
        public const char ManagerInitial = 'N';
        public const char ProductInitial = 'P';
        public const char GroupInitial = 'G';
        public const char ScenarioInitial = 'S';
        public const char SupervisionInitial = 'U';
    }

    public class CommunicationValue
    {
        public const string Power = "PW";
        public const string CTValue = "VU";
        public const string ConnectedMultiStrip = "CM";
        public const string ConnectedAdaptor = "CA";
        public const string DisconnectedAdpator = "DA";
        
        public const string PowerOffValue = "2";
        public const string PowerOnValue = "3";

    }
}
