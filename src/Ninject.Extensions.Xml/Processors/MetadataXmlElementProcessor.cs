//-------------------------------------------------------------------------------
// <copyright file="MetadataXmlElementProcessor.cs" company="Ninject Project Contributors">
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

    using Ninject.Extensions.Xml.Extensions;
    using Ninject.Planning.Bindings;

    /// <summary>
    /// Processor for metadata elements.
    /// </summary>
    public class MetadataXmlElementProcessor : AbstractXmlElementProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataXmlElementProcessor"/> class.
        /// </summary>
        public MetadataXmlElementProcessor()
            : base("metadata", new string[0], Tags.HasMetadata)
        {
        }

        /// <summary>
        /// Processes the specified element.
        /// </summary>
        /// <param name="element">The element that shall be processed.</param>
        /// <param name="owner">The owner of this instance.</param>
        /// <param name="syntax">The binding syntax.</param>
        public override void Process(
            XElement element, 
            IOwnXmlNodeProcessor owner,
            IBindingConfigurationSyntax<object> syntax)
        {
            XAttribute keyAttribute = element.RequiredAttribute("key");
            XAttribute valueAttribute = element.RequiredAttribute("value");

            syntax.WithMetadata(keyAttribute.Value, valueAttribute.Value);
        }
    }
}