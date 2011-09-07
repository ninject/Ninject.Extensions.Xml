namespace Ninject.Extensions.Xml.Processors
{
    using FluentAssertions;

    using Xunit;

    public abstract class AttributeProcessorTestsBase<TTestee> : ProcessorTestsBase<TTestee>
        where TTestee : IXmlAttributeProcessor
    {
        protected AttributeProcessorTestsBase(string expectedOwnerTag, string expectedNodeName)
            : base(expectedOwnerTag, expectedNodeName)
        {
        }

        [Fact]
        public void AttributeIsNotRequired()
        {
            var testee = CreateTestee();
            var required = testee.Required;

            required.Should().BeFalse();
        }
    }
}