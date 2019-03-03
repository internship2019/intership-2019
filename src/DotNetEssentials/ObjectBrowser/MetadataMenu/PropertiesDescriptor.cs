using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectBrowser.MetadataMenu
{
    public class PropertiesDescriptor : IMemberInfoDescriptor
    {
        public IEnumerable<string> Describe(MemberInfo memberInfo)
        {
            yield return "Properties:";

            var properties = memberInfo.GetType().GetProperties();
            var maxPropertyNameLength = properties.Max(x => x.Name.Length);

            var format = "\t{0,-" + maxPropertyNameLength + "}: {1}";

            foreach (var propertyInfo in properties.OrderBy(x => x.MemberType).ThenBy(x => x.Name))
            {
                yield return string.Format(format, propertyInfo.Name, propertyInfo.GetValue(memberInfo));
            }
        }
    }
}
