using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenSmoke.Core
{

}

namespace GreenSmoke.Core.Logic
{
    public enum EnumChildType
    {
        GroupInGroup,
        ManagerInManager,
        CategoryInCategory,
        GroupInManager,
        CategoryInItem        
    }
}

namespace GreenSmoke.Core.Item
{
    public enum EnumItem
    {
        Group,
        MultiStrip,
        Adaptor,
        Product,
        Manager,
        CategoryItem
    }

    public enum EnumPolicy
    {
        Scenario,
        Supervision
    }

    public enum EnumControl
    {
        PowerOn,
        PowerOff,
        Enable,
        Disenable,
        Safe,
        Unsafe
    }

    public enum EnumPowerStateMode
    {
        Idle,
        Sleep,
        Unknow
    }
}

namespace GreenSmoke.Core.Policy
{
    public enum EnumCTValueMode
    {
        High,
        Low,
        Eqaul
    }

    public enum EnumProcessMode
    {
        Alert,
        AlertAndPowerOff,
        PowerOff,
        None
    }

    public enum EnumControlTimeMode
    {
        None,
        PowerOn,
        PowerOff,
        PowerOnAndPowerOff
    }

    public enum EnumControlSpanMode
    {
        None,
        PauseTime,
        PlayTime,
        PauseTimeAndPlayTime
    }
}