// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

namespace Ninject.Extensions.Xml.Extensions
{
    using System;
    using System.Configuration;
    using System.Xml.Linq;

    /// <summary>
    /// Extension methods for XElement
    /// </summary>
    public static class ExtensionsForXElement
    {
        /// <summary>
        /// Gets a required attribute from an XElement.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The attribute</returns>
        public static XAttribute RequiredAttribute(this XElement element, XName name)
        {
            XAttribute attribute = element.Attribute(name);

            if (attribute == null)
            {
                throw new ConfigurationErrorsException(
                    String.Format("The '{0}' element does not have the required attribute '{1}'.", element.Name, name.LocalName));
            }

            return attribute;
        }
    }
}