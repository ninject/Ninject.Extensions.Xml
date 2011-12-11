//-------------------------------------------------------------------------------
// <copyright file="NameXmlAttributeProcessorTest.cs" company="Ninject Project Contributors">
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
#endif