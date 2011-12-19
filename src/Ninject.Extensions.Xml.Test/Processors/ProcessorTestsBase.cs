//-------------------------------------------------------------------------------
// <copyright file="ProcessorTestsBase.cs" company="Ninject Project Contributors">
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

#if !NO_MOQ
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

#if !NO_GENERIC_MOQ
        protected static Mock<IBindingConfigurationSyntax<object>> CreateBindingSyntaxMock()
        {
            return new Mock<IBindingConfigurationSyntax<object>>();
        }
#endif

        protected abstract TTestee CreateTestee();
    }
}
#endif