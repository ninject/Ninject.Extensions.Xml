// -------------------------------------------------------------------------------------------------
// <copyright file="IChildElementProcessor.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Processors
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    using Ninject.Components;
    using Ninject.Planning.Bindings;

    /// <summary>
    /// ¨Processes the child elements of an xml node.
    /// </summary>
    public interface IChildElementProcessor : INinjectComponent
    {
        /// <summary>
        /// Processes the attributes of the given element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="syntax">The binding syntax.</param>
        void ProcessAttributes(
            XElement element,
            IBindingConfigurationSyntax<object> syntax);

        /// <summary>
        /// Processes the attributes of the given element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="syntax">The syntax.</param>
        /// <param name="excludedAttributes">The attributes that are excluded.</param>
        void ProcessAttributes(XElement element, IBindingConfigurationSyntax<object> syntax, IEnumerable<string> excludedAttributes);

        /// <summary>
        /// Processes the child elements.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="syntax">The syntax.</param>
        void ProcessChildElements(
            XElement element,
            IBindingConfigurationSyntax<object> syntax);

        /// <summary>
        /// Sets the owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        void SetOwner(IOwnXmlNodeProcessor owner);
    }
}