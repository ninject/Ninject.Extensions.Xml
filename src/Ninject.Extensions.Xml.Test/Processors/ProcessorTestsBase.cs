namespace Ninject.Extensions.Xml.Processors
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Moq;

    using Ninject.Planning.Bindings;

    using Xunit;

    public abstract class ProcessorTestsBase<TTestee>
        where TTestee : IXmlNodeProcessor
    {
        private readonly string expectedOwnerTag;

        private readonly string expectedNodeName;

        protected ProcessorTestsBase(string expectedOwnerTag, string expectedNodeName)
        {
            this.expectedOwnerTag = expectedOwnerTag;
            this.expectedNodeName = expectedNodeName;
        }

        [Fact]
        public void ProcessorAppliesToOwnersThatHaveHasScopeTag()
        {
            var testee = this.CreateTestee();
            var ownerMock = CreateOwnerMock("ownerName", this.expectedOwnerTag);

            var applies = testee.AppliesTo(ownerMock.Object);

            applies.Should().BeTrue();
        }

        [Fact]
        public void ProcessorAppliesNotToOwnersThatHaveNoHasScopeTag()
        {
            var unexpectedTags = new List<string> { Tags.Binding, Tags.HasCondition, Tags.HasMetadata, Tags.HasName, Tags.HasScope };
            unexpectedTags.Remove(this.expectedOwnerTag);

            var testee = this.CreateTestee();
            var ownerMock = CreateOwnerMock("ownerName", unexpectedTags.ToArray());

            var applies = testee.AppliesTo(ownerMock.Object);

            applies.Should().BeFalse();
        }

        [Fact]
        public void NodeNameIsScope()
        {
            var testee = this.CreateTestee();
            var nodeName = testee.XmlNodeName;

            nodeName.Should().Be(this.expectedNodeName);
        }

        protected static IOwnXmlNodeProcessor CreateOwner()
        {
            return CreateOwnerMock("owner").Object;
        }
        
        protected static Mock<IOwnXmlNodeProcessor> CreateOwnerMock(string name, params string[] tags)
        {
            var ownerMock = new Mock<IOwnXmlNodeProcessor>();
            ownerMock.Setup(o => o.ElementTags).Returns(tags);
            ownerMock.SetupGet(o => o.XmlNodeName).Returns(name);
            return ownerMock;
        }

        protected abstract TTestee CreateTestee();

        protected static Mock<IBindingSyntax<object>> CreateBindingSyntaxMock()
        {
            return new Mock<IBindingSyntax<object>>();
        }
    }
}