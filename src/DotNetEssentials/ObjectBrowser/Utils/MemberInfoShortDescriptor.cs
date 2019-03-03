using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ObjectBrowser.Utils
{
    public static class MemberInfoShortDescriptor
    {
        // keeps instance of StringBuilder per thread. Don't forget to clear it after usage ;-)
        private static readonly ThreadLocal<StringBuilder> StringBuilderLocal = new ThreadLocal<StringBuilder>(() => new StringBuilder(64));

        public static string GetMemberInfoString(MemberInfo memberInfo)
        {
            if (memberInfo is MethodInfo methodInfo)
            {
                var builder = StringBuilderLocal.Value
                    .Append("Method: ")
                    .Append(methodInfo.ReturnType.Name)
                    .Append(' ')
                    .Append(methodInfo.Name)
                    .AppendFormat("({0})", string.Join(", ", methodInfo.GetParameters().Select(parameterInfo => parameterInfo.ParameterType.Name)));

                try
                {
                    return builder.ToString();
                }
                finally
                {
                    builder.Clear();
                }
            }

            return $"{memberInfo.MemberType} {memberInfo.Name}";
        }
    }
}
