// -------------------------------------------------------------------------------------------------
// <copyright file="ExtensionsForDictionary.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Extensions
{
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// Extension methods for IDictionary{string, TProcessor}
    /// </summary>
    public static class ExtensionsForDictionary
    {
        /// <summary>
        /// Gets the processor for the specified element.
        /// </summary>
        /// <typeparam name="TProcessor">The type of the processor.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="parentElementName">Name of the parent element.</param>
        /// <returns>The processor for the specified element.</returns>
        /// <exception cref="XmlException">No processor for the specified element.</exception>
        public static TProcessor GetProcessor<TProcessor>(this IDictionary<string, TProcessor> dictionary, string elementName, string parentElementName)
        {
            if (!dictionary.TryGetValue(elementName, out TProcessor processor))
            {
                var message = $"<{parentElementName}> element contains an unknown element type '{elementName}'.";
                throw new XmlException(message);
            }

            return processor;
        }
    }
}