using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using ObjectBrowser.MetadataMenu;

namespace ObjectBrowserTests.MetadataMenu
{
    public class MethodInfoDescriptorTests
    {
        private MethodInfoDescriptor target;

        [SetUp]
        public void SetUp()
        {
            target = new MethodInfoDescriptor();
        }

        [Test]
        public void Describe_NotAMethod_RetunrNull()
        {
            // Arrange
            var memberInfo = typeof(DummyClassForTesting).GetMembers().First(x => !(x is MethodInfo));

            // Act
            var output = target.Describe(memberInfo);

            // Assert
            Assert.IsNull(output);
        }

        [Test]
        public void Describe_NoParams_ShowNothing()
        {
            // Arrange
            var methodName = nameof(DummyClassForTesting.MethodWithNoParams);
            var methodInfo = typeof(DummyClassForTesting).GetMethod(methodName);

            // Act
            var output = target.Describe(methodInfo);

            // Assert
            Assert.IsFalse(output.Any());
        }

        [Test]
        public void Describe_ShowCorrectParamName()
        {
            // Arrange
            var methodName = nameof(DummyClassForTesting.MethodWithAParam);
            var methodInfo = typeof(DummyClassForTesting).GetMethod(methodName);

            // Act
            var output = target.Describe(methodInfo);

            // Assert
            var paramName = methodInfo.GetParameters().First().Name;
            var rx = new Regex($".*{paramName}.*");

            Assert.IsTrue(LinesMentionPattern(output, rx));
        }

        [Test]
        public void Describe_HasDefaultNullParam_ShowDefaultNull()
        {
            // Arrange
            var methodName = nameof(DummyClassForTesting.MethodWithNullDefaultParameter);
            var methodInfo = typeof(DummyClassForTesting).GetMethod(methodName);

            // Act
            var output = target.Describe(methodInfo);

            // Assert
            var rx = new Regex(@".*Default.*null.*", RegexOptions.IgnoreCase);
            Assert.IsTrue(LinesMentionPattern(output, rx));
        }

        [Test]
        public void Describe_HasDefaultParam_ShowDefaultValue()
        {
            // Arrange
            var methodName = nameof(DummyClassForTesting.MethodWithDefaultParameter);
            var methodInfo = typeof(DummyClassForTesting).GetMethod(methodName);

            // Act
            var output = target.Describe(methodInfo);

            // Assert
            var defaultValue = methodInfo.GetParameters().First().DefaultValue;
            var rx = new Regex($".*Default.*{defaultValue}.*", RegexOptions.IgnoreCase);
            Assert.IsTrue(LinesMentionPattern(output, rx));
        }

        [Test]
        public void Describe_HasOutParameter_ShowParamHasOut()
        {
            // Arrange
            var methodName = nameof(DummyClassForTesting.MethodWithOutParameter);
            var methodInfo = typeof(DummyClassForTesting).GetMethod(methodName);

            // Act
            var output = target.Describe(methodInfo);

            // Assert
            var paramName = methodInfo.GetParameters().First(x => x.IsOut).Name;
            var rx = new Regex($".*{paramName}.*out.*", RegexOptions.IgnoreCase);

            Assert.IsTrue(LinesMentionPattern(output, rx));
        }

        [Test]
        public void Describe_HasStringParamType_ShowCorrectParamType()
        {
            // Arrange
            var methodName = nameof(DummyClassForTesting.MethodWithStrParamType);
            var methodInfo = typeof(DummyClassForTesting).GetMethod(methodName);

            // Act
            var output = target.Describe(methodInfo);

            // Assert
            var paramName = methodInfo.GetParameters().First().Name;
            var rx = new Regex($".*{paramName}.*string.*", RegexOptions.IgnoreCase);

            Assert.IsTrue(LinesMentionPattern(output, rx));
        }

        private bool LinesMentionPattern(IEnumerable<string> lines, Regex pattern)
        {
            var targetLineMentioned = false;
            foreach (var line in lines)
            {
                if (pattern.IsMatch(line))
                {
                    targetLineMentioned = true;
                    break;
                }
            }

            return targetLineMentioned;
        }
    }
}
