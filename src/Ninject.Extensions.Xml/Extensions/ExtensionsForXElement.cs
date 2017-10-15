// -------------------------------------------------------------------------------------------------
// <copyright file="ExtensionsForXElement.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Extensions
{
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// Extension methods for <see cref="XElement"/>
    /// </summary>
    public static class ExtensionsForXElement
    {
        /// <summary>
        /// Gets a required attribute from an <see cref="XElement"/>.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The attribute</returns>
        /// <exception cref="XmlException">A required attribute is missing.</exception>
        public static XAttribute RequiredAttribute(this XElement element, XName name)
        {
            XAttribute attribute = element.Attribute(name);

            if (attribute == null)
            {
                var message = $"The '{element.Name}' element does not have the required attribute '{name.LocalName}'.";
                throw new XmlException(message);
            }

            return attribute;
        }
    }
}