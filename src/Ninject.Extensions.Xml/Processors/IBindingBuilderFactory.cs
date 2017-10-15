// -------------------------------------------------------------------------------------------------
// <copyright file="IBindingBuilderFactory.cs" company="Ninject Project Contributors">
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
    using Ninject.Syntax;

    /// <summary>
    /// Factory for the binding builder.
    /// </summary>
    public interface IBindingBuilderFactory : INinjectComponent
    {
        /// <summary>
        /// Creates a new binding builder and returns its binding syntax.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="module">The module.</param>
        /// <returns>The binding syntax of the created binding builder.</returns>
        IBindingConfigurationSyntax<object> Create(XElement element, IBindingRoot module);
    }
}