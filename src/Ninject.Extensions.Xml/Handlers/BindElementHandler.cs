// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

namespace Ninject.Extensions.Xml.Handlers
{
    using System;
    using System.Configuration;
    using System.Xml.Linq;
    using Ninject.Components;
    using Ninject.Extensions.Xml.Extensions;
    using Ninject.Planning.Bindings;

    /// <summary>
    /// HAndler for the "Bind" Element
    /// </summary>
    public class BindElementHandler : NinjectComponent, IXmlElementHandler
    {
        /// <summary>
        /// The ninject kernel.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindElementHandler"/> class.
        /// </summary>
        /// <param name="kernel">The ninject kernel.</param>
        public BindElementHandler(IKernel kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// Gets the name of the element that is handled by this handler.
        /// </summary>
        /// <value>The name of the element that is handled by this handler.</value>
        public string ElementName
        {
            get { return "bind"; }
        }

        /// <summary>
        /// Handles the XElement.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="element">The element.</param>
        public void Handle(XmlModule module, XElement element)
        {
            XAttribute serviceAttribute = element.RequiredAttribute("service");

            Type service = GetTypeFromAttributeValue(serviceAttribute);

            var binding = new Binding(service);
            var builder = new BindingBuilder<object>(binding, this.kernel);

            module.AddBinding(binding);

            if (!HandleTarget(element, builder))
            {
                throw new ConfigurationErrorsException("The 'bind' element does not define either a 'to' or 'toProvider' attribute.");
            }

            ReadName(element, builder);
            ReadMetadata(element, builder);
            ReadScope(element, builder);
        }

        /// <summary>
        /// Reads the name attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="builder">The builder.</param>
        private static void ReadName(XElement element, BindingBuilder<object> builder)
        {
            var nameAttribute = element.Attribute("name");

            if (nameAttribute != null)
            {
                builder.Named(nameAttribute.Value);
            }
        }

        /// <summary>
        /// Reads the metadata.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="builder">The builder.</param>
        private static void ReadMetadata(XElement element, BindingBuilder<object> builder)
        {
            foreach (XElement metadataElement in element.Elements("metadata"))
            {
                XAttribute keyAttribute = metadataElement.RequiredAttribute("key");
                XAttribute valueAttribute = metadataElement.RequiredAttribute("value");

                builder.WithMetadata(keyAttribute.Value, valueAttribute.Value);
            }
        }

        /// <summary>
        /// Reads the scope.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="builder">The builder.</param>
        private static void ReadScope(XElement element, BindingBuilder<object> builder)
        {
            XAttribute scopeAttribute = element.Attribute("scope");

            if (scopeAttribute == null || String.IsNullOrEmpty(scopeAttribute.Value))
            {
                builder.InTransientScope();
                return;
            }

            string value = scopeAttribute.Value.ToLower();

            switch (value)
            {
                case "transient":
                    builder.InTransientScope();
                    break;

                case "singleton":
                    builder.InSingletonScope();
                    break;

                case "thread":
                    builder.InThreadScope();
                    break;

#if WEB
                case "request":
                    builder.InRequestScope();
                    break;
#endif

                default:
                    throw new ConfigurationErrorsException(String.Format("The 'bind' element has an unknown value '{0}' for its 'scope' attribute. Valid values are transient, singleton, thread, and request.", value));
            }
        }

        /// <summary>
        /// Handles the target attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="builder">The builder.</param>
        /// <returns>True if To or ToProvider was found.</returns>
        private static bool HandleTarget(XElement element, BindingBuilder<object> builder)
        {
            return 
                TryHandleToAttribute(element, builder) || 
                TryHandleToProviderAttribute(element, builder);
        }

        /// <summary>
        /// Tries to handle the to attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="builder">The builder.</param>
        /// <returns>True if the To Attribute was found</returns>
        private static bool TryHandleToAttribute(XElement element, BindingBuilder<object> builder)
        {
            XAttribute toAttribute = element.Attribute("to");

            if (toAttribute == null)
            {
                return false;
            }

            Type implementation = GetTypeFromAttributeValue(toAttribute);
            builder.To(implementation);

            return true;
        }

        /// <summary>
        /// Tries to handle the ToProvider attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="builder">The builder.</param>
        /// <returns>True if the attribute was found.</returns>
        private static bool TryHandleToProviderAttribute(XElement element, BindingBuilder<object> builder)
        {
            XAttribute providerAttribute = element.Attribute("toProvider");

            if (providerAttribute == null)
            {
                return false;
            }

            Type provider = GetTypeFromAttributeValue(providerAttribute);
            builder.ToProvider(provider);

            return true;
        }

        /// <summary>
        /// Gets the type from attribute value.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns>The type specified by the attribute.</returns>
        private static Type GetTypeFromAttributeValue(XAttribute attribute)
        {
            Type service = Type.GetType(attribute.Value, false);

            if (service == null)
            {
                throw new ConfigurationErrorsException(
                    String.Format("Couldn't resolve type '{0}' defined in '{1}' attribute.", attribute.Value, attribute.Name));
            }

            return service;
        }
    }
}