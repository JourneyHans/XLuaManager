using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XLua;

public static class HotfixList
{
    [Hotfix]
    public static List<Type> hotfixlist
    {
        get { return (from type in Assembly.Load("Assembly-CSharp").GetTypes() select type).ToList(); }
    }
}
