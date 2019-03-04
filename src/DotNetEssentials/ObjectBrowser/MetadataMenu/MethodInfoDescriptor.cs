using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ObjectBrowser.Utils;

namespace ObjectBrowser.MetadataMenu
{
    /*
     * Prints the parameters if it's a method.    
     * TODO: Add option to print parameters without their public members.    
    **/ 
    public class MethodInfoDescriptor : IMemberInfoDescriptor
    {
        public IEnumerable<string> Describe(MemberInfo memberInfo)
        {
            if (memberInfo is MethodInfo methodInfo)
                return GetMethodInfoParametersStr(methodInfo);
            else
                return null;
        }

        private IEnumerable<string> GetMethodInfoParametersStr(MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();

            if (!parameters.Any())
                yield break;

            yield return "Parameters:";

            var isFirstParameter = false;
            foreach (var parameterInfo in parameters.OrderBy(x => x.Position))
            {
                var paramDescription = GetParameterDescription(parameterInfo);
                yield return $"\t{parameterInfo.Name}: {paramDescription}";

                var membersShortDescription =
                    GetShortMembersInfoDescription(ReflectionUtils.ExtractMembers(parameterInfo.ParameterType));

                foreach (var line in membersShortDescription)
                    yield return "\t\t" + line;

                // Add empty line between parameters
                if (isFirstParameter)
                    isFirstParameter = false;
                else
                    yield return "";
            }
        }

        private IEnumerable<string> GetShortMembersInfoDescription(IEnumerable<MemberInfo> members)
        {
            foreach (var member in members)
            {
                yield return MemberInfoShortDescriptor.GetMemberInfoString(member);
            }
        }

        private string GetParameterDescription(ParameterInfo parameter)
        {
            var builder = new StringBuilder().Append(parameter.ParameterType).Append(' ');

            if (parameter.IsOut) builder.Append("out ");

            if (parameter.HasDefaultValue)
            {
                builder.Append("Default: " + (parameter.DefaultValue ?? "null"));
                builder.Append(' ');
            }

            return builder.ToString();
        }
    }
}
