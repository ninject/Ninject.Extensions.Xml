//-------------------------------------------------------------------------------
// <copyright file="AbstractXmlElementProcessor.cs" company="Ninject Project Contributors">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using Ninject.Components;
    using Ninject.Planning.Bindings;

    /// <summary>
    /// Apstract base implementation for element processors
    /// </summary>
    public abstract class AbstractXmlElementProcessor : NinjectComponent, IXmlElementProcessor, IOwnXmlNodeProcessor
    {
        /// <summary>
        /// The tags of this element used to decide which processor apply as its children.
        /// </summary>
        private readonly string ownerTag;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractXmlElementProcessor"/> class.
        /// </summary>
        /// <param name="elementName">The name of the element processed by this instance.</param>
        /// <param name="tags">The tags of this element used to decide which processor apply as its children.</param>
        /// <param name="ownerTag">The tag that the oner must have inorder to apply as child processor.</param>
        protected AbstractXmlElementProcessor(string elementName, IEnumerable<string> tags, string ownerTag)
        {
            this.ownerTag = ownerTag;
            this.XmlNodeName = elementName;
            this.ElementTags = tags;
        }

        /// <summary>
        /// Gets the name of the XML node.
        /// </summary>
        /// <value>The name of the XML node.</value>
        public string XmlNodeName { get; private set; }

        /// <summary>
        /// Gets the tags of the elements that apply to this owner as its children.
        /// </summary>
        /// <value>
        /// The tags of the elements that apply to this owner as its children.
        /// </value>
        public IEnumerable<string> ElementTags { get; private set; }

        /// <summary>
        /// Specifies if the processor applies to the given owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns>
        /// True if the processor is a applicable processor for the specified owner.
        /// </returns>
        public virtual bool AppliesTo(IOwnXmlNodeProcessor owner)
        {
            return owner.ElementTags.Contains(this.ownerTag);
        }

        /// <summary>
        /// Processes the specified element.
        /// </summary>
        /// <param name="element">The element that shall be processed.</param>
        /// <param name="owner">The owner of this instance.</param>
        /// <param name="bindingSyntax">The binding syntax.</param>
        public abstract void Process(
            XElement element, 
            IOwnXmlNodeProcessor owner, 
            IBindingSyntax<object> bindingSyntax);
    }
}