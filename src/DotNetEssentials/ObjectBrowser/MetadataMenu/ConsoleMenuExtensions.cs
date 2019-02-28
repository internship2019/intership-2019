using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Intership.Core.ConsoleMenu;

namespace ObjectBrowser.MetadataMenu
{
	public static class ConsoleMenuExtensions
	{
		// keeps instance of StringBuilder per thread. Don't forget to clear it after usage ;-)
		private static readonly ThreadLocal<StringBuilder> StringBuilderLocal = new ThreadLocal<StringBuilder>(() => new StringBuilder(64));

		public static CompositeMenuItem GetAssembliesMenuItem(this IEnumerable<Assembly> assemblies, string header)
		{
			if (assemblies == null)
			{
				throw new ArgumentNullException(nameof(assemblies));
			}

			return new CompositeMenuItem(
				header,
				ExitMenuItem.Instance,
				assemblies.OrderBy(asm => asm.GetName().Name).Select(GetAssemblyTypesMenuItem));
		}

		private static CompositeMenuItem GetAssemblyTypesMenuItem(this Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException(nameof(assembly));
			}

			return new CompositeMenuItem(
				assembly.FullName,
				assembly.GetExportedTypes().OrderBy(x => x.FullName).Select(GetTypeMenuItem));
		}

		public static CompositeMenuItem GetTypeMenuItem(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			return new CompositeMenuItem(
				$"{type.Name} ({type.Namespace})",
				type.GetMembers().OrderBy(x => x.MemberType).ThenBy(x => x.Name).Select(GetMemberMenuItem));
		}

		private static CommandMenuItem GetMemberMenuItem(this MemberInfo memberInfo)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException(nameof(memberInfo));
			}

			return new CommandMenuItem(
				GetMemberInfoString(memberInfo),
				() => PrintMemberInfoProperties(memberInfo));
		}

		private static void PrintMemberInfoProperties(MemberInfo memberInfo)
		{
			var memberInfoType = memberInfo.GetType(); // get concrete type in runtime

			var properties = memberInfoType.GetProperties();

			var maxPropertyNameLength = properties.Max(x => x.Name.Length);

			Console.WriteLine(GetMemberInfoString(memberInfo));

			var format = "\t{0,-" + maxPropertyNameLength.ToString() + "}: {1}";

			foreach (var propertyInfo in properties.OrderBy(x => x.MemberType).ThenBy(x => x.Name))
			{
				Console.WriteLine(format, propertyInfo.Name, propertyInfo.GetValue(memberInfo));
			}
		}

		private static string GetMemberInfoString(MemberInfo memberInfo)
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