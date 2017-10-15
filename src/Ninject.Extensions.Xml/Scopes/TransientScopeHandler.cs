// -------------------------------------------------------------------------------------------------
// <copyright file="TransientScopeHandler.cs" company="Ninject Project Contributors">
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
    /// The processor for the transient scope
    /// </summary>
    public class TransientScopeHandler : NinjectComponent, IScopeHandler
    {
        /// <summary>
        /// The identification name of the transient scope.
        /// </summary>
        public const string Name = "transient";

        /// <summary>
        /// Gets the name of the scope processed by this instance.
        /// </summary>
        /// <value>The name of the scope processed by this instance.</value>
        public string ScopeName
        {
            get
            {
                return Name;
            }
        }

        /// <summary>
        /// Sets the scope using the given syntax.
        /// </summary>
        /// <param name="syntax">The syntax that is used to set the scope.</param>
        public void SetScope(IBindingInSyntax<object> syntax)
        {
            syntax.InTransientScope();
        }
    }
}