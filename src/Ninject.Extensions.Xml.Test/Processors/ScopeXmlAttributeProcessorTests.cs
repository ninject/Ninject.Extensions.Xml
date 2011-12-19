//-------------------------------------------------------------------------------
// <copyright file="ScopeXmlAttributeProcessorTests.cs" company="Ninject Project Contributors">
//   Copyright (c) 2009-2011 Ninject Project Contributors
//   Authors: Remo Gloor (remo.gloor@gmail.com)
//           
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   you may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

#if !NO_GENERIC_MOQ && !NO_MOQ
namespace Ninject.Extensions.Xml.Processors
{
    using System.Configuration;

    using FluentAssertions;

    using Moq;

    using Ninject.Extensions.Xml.Scopes;
    using Ninject.Planning.Bindings;

    using Xunit;

    public class ScopeXmlAttributeProcessorTests : AttributeProcessorTestsBase<ScopeXmlAttributeProcessor>
    {
        public ScopeXmlAttributeProcessorTests()
            : base(Tags.HasScope, "scope")
        {
        }

        [Fact]
        public void HandlingOfAScopeIsForwardedtoAppropriateHandler()
        {
            const string Scope = "TheScope";
            var syntax = CreateBindingSyntaxMock().Object;
            var ownerMock = CreateOwnerMock("ownerName");   

            var scopeHandlerMock = CreateScopeHandlerMock(Scope);
            var testee = CreateTestee(CreateScopeHandler("SomeScope"), scopeHandlerMock.Object, CreateScopeHandler("AnotherScope"));

            testee.Process(Scope, ownerMock.Object, syntax);

            scopeHandlerMock.Verify(h => h.SetScope(syntax));
        }

        [Fact]
        public void IfScopeIsUnknownThenConfigurationErrorsExceptionIsThrown()
        {
            var syntax = new Mock<IBindingConfigurationSyntax<object>>().Object;
            var ownerMock = CreateOwnerMock("owner");

            var testee = CreateTestee(CreateScopeHandler("SomeScope"), CreateScopeHandler("AnotherScope"));

            var exception = Assert.Throws<ConfigurationErrorsException>(() => testee.Process("UnknownScope", ownerMock.Object, syntax));

            exception.Message.Should().Be("The 'owner' element has an unknown value 'UnknownScope' for its 'scope' attribute. Valid values are SomeScope and AnotherScope.");
        }

        protected override ScopeXmlAttributeProcessor CreateTestee()
        {
            return new ScopeXmlAttributeProcessor(new IScopeHandler[0]);
        }

        private static ScopeXmlAttributeProcessor CreateTestee(params IScopeHandler[] scopeHandlers)
        {
            return new ScopeXmlAttributeProcessor(scopeHandlers);
        }
        
        private static IScopeHandler CreateScopeHandler(string scopeName)
        {
            return CreateScopeHandlerMock(scopeName).Object;
        }

        private static Mock<IScopeHandler> CreateScopeHandlerMock(string scopeName)
        {
            var handler = new Mock<IScopeHandler>();
            handler.SetupGet(h => h.ScopeName).Returns(scopeName);
            return handler;
        }
    }
}
#endif