// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

namespace Ninject.Extensions.Xml.Processors
{
    using System.Xml.Linq;

    using Ninject.Components;
    using Ninject.Syntax;

    /// <summary>
    /// Processor for XElements
    /// </summary>
    public interface IModuleChildXmlElementProcessor : IHaveXmlNodeName, INinjectComponent
    {
        /// <summary>
        /// Handles the XElement.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="element">The element.</param>
        void Handle(IBindingRoot module, XElement element);
    }
}