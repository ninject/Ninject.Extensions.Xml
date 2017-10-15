//-------------------------------------------------------------------------------
// <copyright file="ChildElementProcessorTest.cs" company="Ninject Project Contributors">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using FluentAssertions;
    using Moq;
    using Ninject.Planning.Bindings;
    using Xunit;

    public class ChildElementProcessorTest
    {
        private readonly List<IXmlAttributeProcessor> attributeProcessors = new List<IXmlAttributeProcessor>();
        private readonly List<IXmlElementProcessor> elementProcessors = new List<IXmlElementProcessor>();
        private readonly ChildElementProcessor testee;

        private readonly IBindingConfigurationSyntax<object> syntax;
        private readonly XElement element;
        private readonly Mock<IOwnXmlNodeProcessor> ownerMock;

        public ChildElementProcessorTest()
        {
            this.testee = new ChildElementProcessor(this.elementProcessors, this.attributeProcessors);
            this.syntax = new Mock<IBindingConfigurationSyntax<object>>().Object;
            this.element = new XElement("bind");
            this.ownerMock = CreateOwnerMock("bind");
        }

        [Fact]
        public void ProcessChildElementsThrowsExceptionIfAnUnknownElementIsFound()
        {
            this.CreateChildElements("SomeElement");

            this.testee.SetOwner(this.ownerMock.Object);
            Action processChildElements = () => this.testee.ProcessChildElements(this.element, this.syntax);

            processChildElements.ShouldThrow<XmlException>()
                .WithMessage("<bind> element contains an unknown element type 'SomeElement'.");
        }

        [Fact]
        public void ProcessChildElementsThrowsExceptionIfOnlyAnUnapplicapleProcessorExists()
        {
            var elements = this.CreateChildElements("someElement", "unknownElement", "anotherElement");
            var processors = this.CreateProcessors(elements);

            GetProcessorMockForElement(processors, "unknownElement").Setup(p => p.AppliesTo(this.ownerMock.Object)).Returns(false);

            this.testee.SetOwner(this.ownerMock.Object);
            Action processChildElements = () => this.testee.ProcessChildElements(this.element, this.syntax);

            processChildElements.ShouldThrow<XmlException>()
                .WithMessage("<bind> element contains an unknown element type 'unknownElement'.");
        }

        [Fact]
        public void ProcessChildElements()
        {
            var elements = this.CreateChildElements("someElement"/*, "anotherElement"*/);
            var processors = this.CreateProcessors(elements);

            this.testee.SetOwner(this.ownerMock.Object);
            this.testee.ProcessChildElements(this.element, this.syntax);

            this.AssertAllElementProcessorsCalled(processors);
        }
        
        [Fact]
        public void ProcessAttributesThrowsExceptionIfAnUnknownElementIsFound()
        {
            this.AddAttribute("to", "1");

            this.testee.SetOwner(this.ownerMock.Object);
            Action processAttributeAction = () => this.testee.ProcessAttributes(this.element, this.syntax);

            processAttributeAction.ShouldThrow<XmlException>().WithMessage("<bind> element contains an unknown element type 'to'.");
        }

        [Fact]
        public void ProcessAttributesThrowsExceptionIfOnlyAnUnapplicapleProcessorExists()
        {
            var attributeData = new List<AttributeData> { new AttributeData("to", "1"), new AttributeData("from", "2") };
            var processors = this.CreateProcessors(attributeData);
            this.AddAttributes(attributeData);

            GetProcessorMockForTag(processors, "to").Setup(p => p.AppliesTo(this.ownerMock.Object)).Returns(false);

            this.testee.SetOwner(this.ownerMock.Object);
            Action processAttributeAction = () => this.testee.ProcessAttributes(this.element, this.syntax);

            processAttributeAction.ShouldThrow<XmlException>()
                .WithMessage("<bind> element contains an unknown element type 'to'.");
        }

        [Fact]
        public void ProcessAttributes()
        {
            var attributeData = new List<AttributeData> { new AttributeData("to", "1"), new AttributeData("from", "2") };
            var processors = this.CreateProcessors(attributeData);
            this.AddAttributes(attributeData);

            this.testee.SetOwner(this.ownerMock.Object);
            this.testee.ProcessAttributes(this.element, this.syntax);

            this.AssertAllAttributeProcessorsCalled(processors);
        }

        [Fact]
        public void ProcessAttributesIfRequiredProcessorWasNotCalledThenExceptionIsThrown()
        {
            var givenAttributeData = new List<AttributeData> { new AttributeData("to", "1"), new AttributeData("from", "2") };
            var expectedAttributeData = givenAttributeData.Union(new List<AttributeData> { new AttributeData("required", "1") });
            this.CreateProcessors(expectedAttributeData);
            this.AddAttributes(givenAttributeData);

            this.testee.SetOwner(this.ownerMock.Object);
            Action processAttributesAction = () => this.testee.ProcessAttributes(this.element, this.syntax);

            processAttributesAction.ShouldThrow<XmlException>()
                .WithMessage("Required attributes for element <bind> not found: required");
        }

        [Fact]
        public void ProcessAttributesIfNotRequiredProcessorWasNotCalledThenNoExceptionIsThrown()
        {
            var givenAttributeData = new List<AttributeData> { new AttributeData("to", "1"), new AttributeData("from", "2") };
            var expectedAttributeData = givenAttributeData.Union(new List<AttributeData> { new AttributeData("notrequired", "1") });
            var processors = this.CreateProcessors(expectedAttributeData);
            SetProcessorToNotRequired(processors, "notrequired");
            this.AddAttributes(givenAttributeData);

            this.testee.SetOwner(this.ownerMock.Object);
            Action processAttributesAction = () => this.testee.ProcessAttributes(this.element, this.syntax);

            processAttributesAction.ShouldNotThrow();
        }

        [Fact]
        public void ProcessAttributesExcludedAttributesAreNotProcessed()
        {
            var attributeData = new List<AttributeData> { new AttributeData("to", "1"), new AttributeData("from", "2") };
            var processors = this.CreateProcessors(attributeData);
            SetProcessorToNotRequired(processors, "from");
            this.AddAttributes(attributeData);

            this.testee.SetOwner(this.ownerMock.Object);
            this.testee.ProcessAttributes(this.element, this.syntax, new[] { "from" });

            GetProcessorMockForTag(processors, "from")
                .Verify(p => p.Process(It.IsAny<string>(), It.IsAny<IOwnXmlNodeProcessor>(), It.IsAny<IBindingConfigurationSyntax<object>>()), Times.Never());
        }

        private static void SetProcessorToNotRequired(IEnumerable<AttributeProcessorData> processors, string tag)
        {
            GetProcessorMockForTag(processors, tag).SetupGet(p => p.Required).Returns(false);
        }

        private static Mock<IXmlElementProcessor> GetProcessorMockForElement(IEnumerable<ElementProcessorData> processors, string elementName)
        {
            return processors.Select(p => p.Processor).Single(p => p.Object.XmlNodeName == elementName);
        }

        private static Mock<IXmlAttributeProcessor> GetProcessorMockForTag(IEnumerable<AttributeProcessorData> processors, string tag)
        {
            return processors.Select(p => p.Processor).Single(p => p.Object.XmlNodeName == tag);
        }
        
        private static Mock<IOwnXmlNodeProcessor> CreateOwnerMock(string elementName)
        {
            var ownerMock = new Mock<IOwnXmlNodeProcessor>();
            ownerMock.Setup(o => o.XmlNodeName).Returns(elementName);
            return ownerMock;
        }

        private IEnumerable<XElement> CreateChildElements(params string[] elementNames)
        {
            var elements = elementNames.Select(n => new XElement(n));
            this.element.Add(elements);
            return elements;
        }
        
        private IEnumerable<ElementProcessorData> CreateProcessors(IEnumerable<XElement> elements)
        {
            return elements.Select(e => new ElementProcessorData(this.AddElementProcessor(e.Name.LocalName), e)).ToList();
        }

        private Mock<IXmlElementProcessor> AddElementProcessor(string processorName)
        {
            var processor = new Mock<IXmlElementProcessor>();
            processor.Setup(p => p.XmlNodeName).Returns(processorName);
            processor.Setup(p => p.AppliesTo(this.ownerMock.Object)).Returns(true);
            this.elementProcessors.Add(processor.Object);

            return processor;
        }

        private void AssertAllElementProcessorsCalled(IEnumerable<ElementProcessorData> processors)
        {
            foreach (var processor in processors)
            {
                processor.Processor.Verify(p => p.Process(It.Is<XElement>(e => e.Name == processor.ExpectedElement.Name), this.ownerMock.Object, this.syntax));
            }
        }

        private void AssertAllAttributeProcessorsCalled(IEnumerable<AttributeProcessorData> processors)
        {
            foreach (var processor in processors)
            {
                processor.Processor.Verify(p => p.Process(processor.ExpectedValue, this.ownerMock.Object, this.syntax));
            }
        }

        private IEnumerable<AttributeProcessorData> CreateProcessors(IEnumerable<AttributeData> attributeData)
        {
            return attributeData.Select(d => new AttributeProcessorData(this.AddAttributeProcessor(d.Tag, true), d.Value)).ToList();
        }

        private void AddAttributes(IEnumerable<AttributeData> attributeData)
        {
            foreach (var data in attributeData)
            {
                this.AddAttribute(data.Tag, data.Value);
            }
        }
        
        private Mock<IXmlAttributeProcessor> AddAttributeProcessor(string processorName, bool required)
        {
            var processor = new Mock<IXmlAttributeProcessor>();
            processor.Setup(p => p.Required).Returns(required);
            processor.Setup(p => p.XmlNodeName).Returns(processorName);
            processor.Setup(p => p.AppliesTo(this.ownerMock.Object)).Returns(true);
            this.attributeProcessors.Add(processor.Object);

            return processor;
        }

        private void AddAttribute(string attributeName, string value)
        {
            var attribute = new XAttribute(attributeName, value);
            this.element.Add(attribute);
        }

        private class AttributeData
        {
            public AttributeData(string tag, string value)
            {
                this.Tag = tag;
                this.Value = value;
            }

            public string Tag { get; private set; }

            public string Value { get; private set; }
        }

        private class AttributeProcessorData
        {
            public AttributeProcessorData(Mock<IXmlAttributeProcessor> processor, string expectedValue)
            {
                this.Processor = processor;
                this.ExpectedValue = expectedValue;
            }

            public Mock<IXmlAttributeProcessor> Processor { get; private set; }

            public string ExpectedValue { get; private set; }
        }
        
        private class ElementProcessorData
        {
            public ElementProcessorData(Mock<IXmlElementProcessor> processor, XElement expectedElement)
            {
                this.Processor = processor;
                this.ExpectedElement = expectedElement;
            }

            public Mock<IXmlElementProcessor> Processor { get; private set; }

            public XElement ExpectedElement { get; private set; }
        }
    }
}
#endif