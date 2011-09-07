namespace Ninject.Extensions.Xml.Processors
{
    using System.Configuration;
    using System.Xml.Linq;

    using FluentAssertions;

    using Xunit;

    public class MetadataXmlElementProcessorTest : ProcessorTestsBase<MetadataXmlElementProcessor>
    {
        public MetadataXmlElementProcessorTest()
            : base(Tags.HasMetadata, "metadata")
        {
        }

        [Fact]
        public void KeyAndValueAttributeAreUsedToSetMetadataOnBinding()
        {
            const string Key = "TheKey";
            const string Value = "TheValue";

            var element = new XElement("metadata");
            element.Add(new XAttribute("key", Key));
            element.Add(new XAttribute("value", Value));

            var syntaxMock = CreateBindingSyntaxMock();
            var testee = this.CreateTestee();
            
            testee.Process(element, CreateOwner(), syntaxMock.Object);

            syntaxMock.Verify(s => s.WithMetadata(Key, Value));
        }

        [Fact]
        public void KeyAttributeIsRequired()
        {
            var element = new XElement("metadata");
            element.Add(new XAttribute("value", "Value"));

            var syntaxMock = CreateBindingSyntaxMock();
            var testee = this.CreateTestee();

            var exception = Assert.Throws<ConfigurationErrorsException>(() => testee.Process(element, CreateOwner(), syntaxMock.Object));

            exception.Message.Should().Be("The 'metadata' element does not have the required attribute 'key'.");
        }

        [Fact]
        public void ValueAttributeIsRequired()
        {
            var element = new XElement("metadata");
            element.Add(new XAttribute("key", "Key"));

            var syntaxMock = CreateBindingSyntaxMock();
            var testee = this.CreateTestee();

            var exception = Assert.Throws<ConfigurationErrorsException>(() => testee.Process(element, CreateOwner(), syntaxMock.Object));

            exception.Message.Should().Be("The 'metadata' element does not have the required attribute 'value'.");
        }
        
        protected override MetadataXmlElementProcessor CreateTestee()
        {
            return new MetadataXmlElementProcessor();
        }
    }
}