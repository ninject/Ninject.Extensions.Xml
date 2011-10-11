//-------------------------------------------------------------------------------
// <copyright file="ChildElementProcessor.cs" company="Ninject Project Contributors">
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
    using System.Configuration;
    using System.Linq;
    using System.Xml.Linq;

    using Ninject.Components;
    using Ninject.Extensions.Xml.Extensions;
    using Ninject.Planning.Bindings;

    /// <summary>
    /// Processes the child nodes of an element.
    /// </summary>
    public class ChildElementProcessor : NinjectComponent, IChildElementProcessor
    {
        private readonly IEnumerable<IXmlElementProcessor> allElementProcessors;

        private readonly IEnumerable<IXmlAttributeProcessor> allAttributeProcessors;

        /// <summary>
        /// The owner of this instance.
        /// </summary>
        private IOwnXmlNodeProcessor owner;

        /// <summary>
        /// The element processors.
        /// </summary>
        private IDictionary<string, IXmlElementProcessor> elementProcessors;

        /// <summary>
        /// The attribute processors.
        /// </summary>
        private IDictionary<string, IXmlAttributeProcessor> attributeProcessors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChildElementProcessor"/> class.
        /// </summary>
        /// <param name="elementProcessors">The element processors.</param>
        /// <param name="attributeProcessors">The attribute processors.</param>
        public ChildElementProcessor(
            IEnumerable<IXmlElementProcessor> elementProcessors,
            IEnumerable<IXmlAttributeProcessor> attributeProcessors)
        {
            this.allElementProcessors = elementProcessors;
            this.allAttributeProcessors = attributeProcessors;
        }

        /// <summary>
        /// Processes the attributes of the given element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="syntax">The binding syntax.</param>
        public void ProcessAttributes(
            XElement element,
            IBindingSyntax<object> syntax)
        {
            this.ProcessAttributes(element, syntax, Enumerable.Empty<string>());
        }

        /// <summary>
        /// Processes the attributes of the given element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="syntax">The syntax.</param>
        /// <param name="excludedAttributes">The attributes that are excluded.</param>
        public void ProcessAttributes(XElement element, IBindingSyntax<object> syntax, IEnumerable<string> excludedAttributes)
        {
            var requiredAttributeProcessors = this.attributeProcessors.Values.Where(processor => processor.Required).ToList();
            foreach (var attribute in element.Attributes().Where(a => !excludedAttributes.Contains(a.Name.LocalName)))
            {
                var processor = this.attributeProcessors.GetProcessor(attribute.Name.LocalName, this.owner.XmlNodeName);
                processor.Process(attribute.Value, this.owner, syntax);
                requiredAttributeProcessors.Remove(processor);
            }

            if (requiredAttributeProcessors.Any())
            {
                throw new ConfigurationErrorsException(
                    string.Format(
                        "Required attributes for element <{0}> not found: {1}",
                        element.Name,
                        string.Join(", ", requiredAttributeProcessors.Select(processor => processor.XmlNodeName).ToArray())));
            }
        }
        
        /// <summary>
        /// Processes the child elements.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="syntax">The syntax.</param>
        public void ProcessChildElements(
            XElement element,
            IBindingSyntax<object> syntax)
        {
            foreach (var childElement in element.Elements())
            {
                var processor = this.elementProcessors.GetProcessor(childElement.Name.LocalName, this.owner.XmlNodeName);
                processor.Process(childElement, this.owner, syntax);
            }
        }

        /// <summary>
        /// Sets the owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public void SetOwner(IOwnXmlNodeProcessor owner)
        {
            this.owner = owner;
            this.elementProcessors = GetRelevantProcessors(owner, this.allElementProcessors);
            this.attributeProcessors = GetRelevantProcessors(owner, this.allAttributeProcessors);
        }

        /// <summary>
        /// Gets the relevant processors.
        /// </summary>
        /// <typeparam name="TNodeType">The type of the node type.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="nodeProcessors">The node processors.</param>
        /// <returns>The relevant processors for the given owner.</returns>
        private static Dictionary<string, TNodeType> GetRelevantProcessors<TNodeType>(
            IOwnXmlNodeProcessor owner,
            IEnumerable<TNodeType> nodeProcessors)
            where TNodeType : IXmlNodeProcessor
        {
            return nodeProcessors
                .Where(processor => processor.AppliesTo(owner))
                .ToDictionary(processor => processor.XmlNodeName);
        }
    }
}