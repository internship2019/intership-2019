using System;
using System.Collections.Generic;
using System.Reflection;

namespace ObjectBrowser.MetadataMenu
{
    public interface IMemberInfoDescriptor
    {
        IEnumerable<String> Describe(MemberInfo memberInfo);
    }
}
