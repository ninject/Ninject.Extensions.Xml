// -------------------------------------------------------------------------------------------------
// <copyright file="BindingBuilderFactory.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd. All rights reserved.
//   Copyright (c) 2009-2017 Ninject Project Contributors. All rights reserved.
//
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   You may not use this file except in compliance with one of the Licenses.
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
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Processors
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    using Ninject.Components;
    using Ninject.Extensions.Xml.Extensions;
    using Ninject.Planning.Bindings;
    using Ninject.Syntax;

    /// <summary>
    /// Factory for the binding builder.
    /// </summary>
    public class BindingBuilderFactory : NinjectComponent, IBindingBuilderFactory
    {
        /// <summary>
        /// Creates a new binding builder and returns its binding syntax.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="module">The module.</param>
        /// <returns>
        /// The binding syntax of the created binding builder.
        /// </returns>
        public IBindingConfigurationSyntax<object> Create(XElement element, IBindingRoot module)
        {
            XAttribute serviceAttribute = element.RequiredAttribute("service");
            Type service = GetTypeFromAttributeValue(serviceAttribute);

            VerifyElementHasExactlyOneToAttribute(element);

            var bindToSyntax = module.Bind(service);
            var syntax = HandleToAttribute(element, bindToSyntax) ?? HandleToProviderAttribute(element, bindToSyntax);

            return (IBindingConfigurationSyntax<object>)syntax;
        }

        /// <summary>
        /// Tries to handle the to attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="builder">The builder.</param>
        /// <returns>True if the To Attribute was found</returns>
        private static IBindingWhenInNamedWithOrOnSyntax<object> HandleToAttribute(XElement element, IBindingToSyntax<object> builder)
        {
            XAttribute toAttribute = element.Attribute("to");

            if (toAttribute == null)
            {
                return null;
            }

            Type implementation = GetTypeFromAttributeValue(toAttribute);
            return builder.To(implementation);
        }

        /// <summary>
        /// Tries to handle the ToProvider attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="builder">The builder.</param>
        /// <returns>True if the attribute was found.</returns>
        private static IBindingWhenInNamedWithOrOnSyntax<object> HandleToProviderAttribute(XElement element, IBindingToSyntax<object> builder)
        {
            XAttribute providerAttribute = element.Attribute("toProvider");

            if (providerAttribute == null)
            {
                return null;
            }

            Type provider = GetTypeFromAttributeValue(providerAttribute);
            return builder.ToProvider(provider);
        }

        /// <summary>
        /// Gets the type from attribute value.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns>The type specified by the attribute.</returns>
        /// <exception cref="XmlException">Thrown if the type can not be resolved.</exception>
        private static Type GetTypeFromAttributeValue(XAttribute attribute)
        {
            Type service = Type.GetType(attribute.Value, false);

            if (service == null)
            {
                throw new XmlException(
                    string.Format("Couldn't resolve type '{0}' defined in '{1}' attribute.", attribute.Value, attribute.Name));
            }

            return service;
        }

        /// <summary>
        /// Verifies that the specified element has exactly one "to" attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <exception cref="XmlException">The 'bind' element does not define either a 'to' or 'toProvider' attribute or it specifies both at the same time.</exception>
        private static void VerifyElementHasExactlyOneToAttribute(XElement element)
        {
            int toAttributeCount = element.Attributes().Where(a => a.Name == "to" | a.Name == "toProvider").Count();
            if (toAttributeCount == 0)
            {
                throw new XmlException("The 'bind' element does not define either a 'to' or 'toProvider' attribute.");
            }

            if (toAttributeCount > 1)
            {
                throw new XmlException("The 'bind' element has both a 'to' and a 'toProvider' attribute. Specify only one of them!");
            }
        }
    }
}