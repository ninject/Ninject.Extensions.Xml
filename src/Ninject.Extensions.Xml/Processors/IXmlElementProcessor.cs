// -------------------------------------------------------------------------------------------------
// <copyright file="IXmlElementProcessor.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Processors
{
    using System.Xml.Linq;

    using Ninject.Components;
    using Ninject.Planning.Bindings;

    /// <summary>
    /// Processor for a xml element
    /// </summary>
    public interface IXmlElementProcessor : IXmlNodeProcessor, INinjectComponent
    {
        /// <summary>
        /// Handles the specified element.
        /// </summary>
        /// <param name="element">The element that shall be processed.</param>
        /// <param name="owner">The owner of this instance.</param>
        /// <param name="bindingSyntax">The binding syntax.</param>
        void Process(
            XElement element,
            IOwnXmlNodeProcessor owner,
            IBindingConfigurationSyntax<object> bindingSyntax);
    }
}