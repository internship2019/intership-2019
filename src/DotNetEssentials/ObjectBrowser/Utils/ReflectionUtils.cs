using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectBrowser.Utils
{
    public static class ReflectionUtils
    {
        public static IEnumerable<MemberInfo> ExtractMembers(Type type)
        {
            return type.GetMembers().OrderBy(x => x.MemberType).ThenBy(x => x.Name);
        }
    }
}
