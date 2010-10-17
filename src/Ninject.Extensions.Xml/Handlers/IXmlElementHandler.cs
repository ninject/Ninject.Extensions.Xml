// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

namespace Ninject.Extensions.Xml.Handlers
{
    using System.Xml.Linq;
    using Ninject.Components;

    /// <summary>
    /// Handler for XElements
    /// </summary>
    public interface IXmlElementHandler : INinjectComponent
    {
        /// <summary>
        /// Gets the name of the element that is handled by this handler.
        /// </summary>
        /// <value>The name of the element that is handled by this handler.</value>
        string ElementName { get; }

        /// <summary>
        /// Handles the XElement.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="element">The element.</param>
        void Handle(XmlModule module, XElement element);
    }
}