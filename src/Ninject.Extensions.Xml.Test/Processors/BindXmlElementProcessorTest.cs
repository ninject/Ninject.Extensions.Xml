//-------------------------------------------------------------------------------
// <copyright file="BindXmlElementProcessorTest.cs" company="Ninject Project Contributors">
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

namespace Ninject.Extensions.Xml.Processors
{
    using System.Xml.Linq;

    using FluentAssertions;

    using Moq;

    using Ninject.Planning.Bindings;
    using Ninject.Syntax;

    using Xunit;

    public class BindXmlElementProcessorTest
    {
        private readonly Mock<IChildElementProcessor> childElementProcessorMock;
        private readonly BindXmlElementProcessor testee;
        private readonly Mock<IBindingBuilderFactory> bindingBuilerFactoryMock;

        public BindXmlElementProcessorTest()
        {
            this.bindingBuilerFactoryMock = new Mock<IBindingBuilderFactory>();
            this.childElementProcessorMock = new Mock<IChildElementProcessor>();
            this.testee = new BindXmlElementProcessor(this.bindingBuilerFactoryMock.Object, this.childElementProcessorMock.Object);
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