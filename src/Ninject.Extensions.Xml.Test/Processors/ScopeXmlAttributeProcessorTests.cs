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
            var syntax = new Mock<IBindingSyntax<object>>().Object;
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