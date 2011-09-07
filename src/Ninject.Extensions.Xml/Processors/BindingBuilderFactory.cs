namespace Ninject.Extensions.Xml.Processors
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Xml.Linq;

    using Ninject.Components;
    using Ninject.Extensions.Xml.Extensions;
    using Ninject.Planning.Bindings;
    using Ninject.Syntax;

    public class BindingBuilderFactory : NinjectComponent, IBindingBuilderFactory
    {
        public IBindingSyntax<object> Create(XElement element, IBindingRoot module)
        {
            XAttribute serviceAttribute = element.RequiredAttribute("service");
            Type service = GetTypeFromAttributeValue(serviceAttribute);

            this.VerifyElementHasExactlyOneToAttribute(element);

            var bindToSyntax = module.Bind(service);
            var syntax = HandleToAttribute(element, bindToSyntax) ?? HandleToProviderAttribute(element, bindToSyntax);

            return (IBindingSyntax<object>)syntax;
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
        /// <exception cref="ConfigurationErrorsException">Thrown if the type can not be resolved.</exception>
        private static Type GetTypeFromAttributeValue(XAttribute attribute)
        {
            Type service = Type.GetType(attribute.Value, false);

            if (service == null)
            {
                throw new ConfigurationErrorsException(
                    string.Format("Couldn't resolve type '{0}' defined in '{1}' attribute.", attribute.Value, attribute.Name));
            }

            return service;
        }

        private void VerifyElementHasExactlyOneToAttribute(XElement element)
        {
            int toAttributeCount = element.Attributes().Where(a => a.Name == "to" | a.Name == "toProvider").Count();
            if (toAttributeCount == 0)
            {
                throw new ConfigurationErrorsException("The 'bind' element does not define either a 'to' or 'toProvider' attribute.");
            }

            if (toAttributeCount > 1)
            {
                throw new ConfigurationErrorsException("The 'bind' element has both a 'to' and a 'toProvider' attribute. Specify only one of them!");
            }
        }
    }
}