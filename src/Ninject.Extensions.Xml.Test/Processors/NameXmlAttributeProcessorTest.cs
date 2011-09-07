namespace Ninject.Extensions.Xml.Processors
{
    using Xunit;

    public class NameXmlAttributeProcessorTest : AttributeProcessorTestsBase<NameXmlAttributeProcessor>
    {
        public NameXmlAttributeProcessorTest()
            : base(Tags.HasName, "name")
        {
        }

        [Fact]
        public void NameIsAssignedToTheBinding()
        {
            const string Name = "TheName";
            var syntaxMock = CreateBindingSyntaxMock();
            var testee = this.CreateTestee();

            testee.Process(Name, CreateOwner(), syntaxMock.Object);

            syntaxMock.Verify(s => s.Named(Name));
        }

        protected override NameXmlAttributeProcessor CreateTestee()
        {
            return new NameXmlAttributeProcessor();
        }
    }
}