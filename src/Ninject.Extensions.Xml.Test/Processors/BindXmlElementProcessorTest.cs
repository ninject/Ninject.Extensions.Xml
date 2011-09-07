namespace Ninject.Extensions.Xml.Processors
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    using FluentAssertions;

    using Moq;

    using Ninject.Planning.Bindings;
    using Ninject.Syntax;

    using Xunit;

    public class BindXmlElementProcessorTest
    {
        private readonly Mock<IChildElementProcessor> childElementProcessorMock;
        private readonly Mock<IKernel> kernelMock;
        private readonly BindXmlElementProcessor testee;
        private readonly Mock<IBindingBuilderFactory> bindingBuilerFactoryMock;


        public BindXmlElementProcessorTest()
        {
            this.bindingBuilerFactoryMock = new Mock<IBindingBuilderFactory>();
            this.kernelMock = new Mock<IKernel>();
            this.childElementProcessorMock = new Mock<IChildElementProcessor>();
            this.testee = new BindXmlElementProcessor(this.kernelMock.Object, this.bindingBuilerFactoryMock.Object, this.childElementProcessorMock.Object);
        }

        [Fact]
        public void ChildElementProcessorIsInitializedCorrectly()
        {
            this.childElementProcessorMock.Verify(cep => cep.SetOwner(this.testee));
        }
        
        [Fact]
        public void ElementTags()
        {
            this.testee.ElementTags.Should()
                .BeEquivalentTo(new[] { Tags.Binding, Tags.HasCondition, Tags.HasMetadata, Tags.HasName, Tags.HasScope });
        }
    
        [Fact]
        public void XmlNodeName()
        {
            this.testee.XmlNodeName.Should().Be("bind");
        }

        [Fact]
        public void HandleCreatesBindingAndProcessesChildElements()
        {
            var module = new Mock<IBindingRoot>().Object;
            var element = new XElement("test");
            var bindingBuilder = new Mock<IBindingSyntax<object>>().Object;
            
            this.bindingBuilerFactoryMock.Setup(f => f.Create(element, module)).Returns(bindingBuilder);

            this.testee.Handle(module, element);

            this.childElementProcessorMock.Verify(
                childElementProcessor => childElementProcessor.ProcessAttributes(element, bindingBuilder, new[] { "service", "to", "toProvider" }));
            this.childElementProcessorMock.Verify(
                childElementProcessor => childElementProcessor.ProcessChildElements(element, bindingBuilder));            
        }
    }
}