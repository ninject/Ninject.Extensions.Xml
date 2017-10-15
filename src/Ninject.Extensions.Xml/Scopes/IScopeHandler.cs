// -------------------------------------------------------------------------------------------------
// <copyright file="IScopeHandler.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Scopes
{
    using Ninject.Components;
    using Ninject.Syntax;

    /// <summary>
    /// Processor for a specific scope type.
    /// </summary>
    public interface IScopeHandler : INinjectComponent
    {
        /// <summary>
        /// Gets the name of the scope processed by this instance.
        /// </summary>
        /// <value>The name of the scope processed by this instance.</value>
        string ScopeName { get; }

        /// <summary>
        /// Sets the scope using the given syntax.
        /// </summary>
        /// <param name="syntax">The syntax that is used to set the scope.</param>
        void SetScope(IBindingInSyntax<object> syntax);
    }
}