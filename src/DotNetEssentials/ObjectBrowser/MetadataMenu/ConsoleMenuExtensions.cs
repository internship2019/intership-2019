using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Intership.Core.ConsoleMenu;
using ObjectBrowser.Utils;

namespace ObjectBrowser.MetadataMenu
{
	public static class ConsoleMenuExtensions
	{
		public static CompositeMenuCommand GetAssembliesMenuItem(this IEnumerable<Assembly> assemblies, string header, IMenuCommand parent = null)
		{
			if (assemblies == null)
			{
				throw new ArgumentNullException(nameof(assemblies));
			}

			var result = new CompositeMenuCommand(header, parent ?? ExitMenuCommand.Instance);

			var commands = assemblies
				.OrderBy(asm => asm.GetName().Name)
				.Select(x => GetAssemblyTypesMenuItem(x, result));

			result.AddCommands(commands);

			return result;
		}

		private static CompositeMenuCommand GetAssemblyTypesMenuItem(this Assembly assembly, IMenuCommand parent)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException(nameof(assembly));
			}

			var result = new CompositeMenuCommand(assembly.FullName, parent);

			result.AddCommands(assembly.GetExportedTypes().OrderBy(x => x.FullName).Select(x => GetTypeMenuItem(x, result)));

			return result;
		}

		private static CompositeMenuCommand GetTypeMenuItem(this Type type, IMenuCommand parent)
		{
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			var result = new CompositeMenuCommand(type.FullName, parent);

			result.AddCommands(ReflectionUtils.ExtractMembers(type).Select(x => GetMemberMenuItem(x, result)));

			return result;
		}

		private static ActionMenuCommand GetMemberMenuItem(this MemberInfo memberInfo, IMenuCommand parent)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException(nameof(memberInfo));
			}

			return new ActionMenuCommand(
				MemberInfoShortDescriptor.GetMemberInfoString(memberInfo),
				() => PrintMemberInfo(memberInfo))
			{
				Parent = parent
			};
		}

		private static void PrintMemberInfo(MemberInfo memberInfo)
		{
            Console.WriteLine(MemberInfoShortDescriptor.GetMemberInfoString(memberInfo));

            // This should be initialized somewhere else
            var descriptors = new IMemberInfoDescriptor[] { new MethodInfoDescriptor(), new PropertiesDescriptor() };

            foreach (var descriptor in descriptors)
                PrintDescription(descriptor, memberInfo);
        }

        /*
         * Extracts the description from the descriptor and prints it.
         * If the description is not empty, print an additional empty line.        
        **/
        private static void PrintDescription(IMemberInfoDescriptor descriptor, MemberInfo memberInfo)
        {
            var description = descriptor.Describe(memberInfo);

            if (description == null)
                return;

            var descriptionIsEmpty = true;
            foreach (var descriptionLine in description)
            {
                Console.WriteLine(descriptionLine);

                if (descriptionIsEmpty) descriptionIsEmpty = false;
            }

            if (!descriptionIsEmpty)
                Console.WriteLine();
        }
    }
}