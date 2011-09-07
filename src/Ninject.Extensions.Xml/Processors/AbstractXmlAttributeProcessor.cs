// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractXmlAttributeProcessor.cs" company="Ninject Project Contributors">
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
// --------------------------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Processors
{
    using System.Linq;

    using Ninject.Components;
    using Ninject.Planning.Bindings;

    /// <summary>
    /// Abstract base implementation for attribute processors.
    /// </summary>
    public abstract class AbstractXmlAttributeProcessor : NinjectComponent, IXmlAttributeProcessor
    {
        /// <summary>
        /// The tag that is expected on the owner to apply as child attribute processor.
        /// </summary>
        private readonly string parentTag;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractXmlAttributeProcessor"/> class.
        /// </summary>
        /// <param name="attributeName">The name of the attribute processed by this processor.</param>
        /// <param name="required">if set to <c>true</c> the attribute is required.</param>
        /// <param name="parentTag">The tag that is expected on the owner to apply as child attribute processor.</param>
        protected AbstractXmlAttributeProcessor(string attributeName, bool required, string parentTag)
        {
            this.parentTag = parentTag;
            this.XmlNodeName = attributeName;
            this.Required = required;
        }

        /// <summary>
        /// Gets the name of the XML node.
        /// </summary>
        /// <value>The name of the XML node.</value>
        public string XmlNodeName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the attribute processed by this instance is required.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the attribute processed by this instance is required; otherwise, <c>false</c>.
        /// </value>
        public bool Required { get; private set; }

        /// <summary>
        /// Specifies if the processor applies to the given owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns>
        /// True if the processor is a applicable processor for the specified owner.
        /// </returns>
        public bool AppliesTo(IOwnXmlNodeProcessor owner)
        {
            return owner.ElementTags.Contains(this.parentTag);
        }

        /// <summary>
        /// Handles the attribute.
        /// </summary>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="owner">The owner of this instance.</param>
        /// <param name="syntax">The binding syntax.</param>
        public abstract void Process(
            string value, 
            IOwnXmlNodeProcessor owner, 
            IBindingSyntax<object> syntax);
    }
}