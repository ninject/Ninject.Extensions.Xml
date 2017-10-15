// -------------------------------------------------------------------------------------------------
// <copyright file="IXmlAttributeProcessor.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Processors
{
    using Ninject.Components;
    using Ninject.Planning.Bindings;

    /// <summary>
    /// Processor for a xml attribute.
    /// </summary>
    public interface IXmlAttributeProcessor : IXmlNodeProcessor, INinjectComponent
    {
        /// <summary>
        /// Gets a value indicating whether the attribute processed by this instance is required.
        /// </summary>
        /// <value><c>true</c> if the attribute processed by this instance is required.; otherwise, <c>false</c>.</value>
        bool Required { get; }

        /// <summary>
        /// Handles the attribute.
        /// </summary>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="owner">The owner of this instance.</param>
        /// <param name="syntax">The binding syntax.</param>
        void Process(string value, IOwnXmlNodeProcessor owner, IBindingConfigurationSyntax<object> syntax);
    }
}