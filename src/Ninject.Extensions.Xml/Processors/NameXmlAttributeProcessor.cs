// -------------------------------------------------------------------------------------------------
// <copyright file="NameXmlAttributeProcessor.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Processors
{
    using Ninject.Planning.Bindings;

    /// <summary>
    /// Processor for the name attribute
    /// </summary>
    public class NameXmlAttributeProcessor : AbstractXmlAttributeProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameXmlAttributeProcessor"/> class.
        /// </summary>
        public NameXmlAttributeProcessor()
            : base("name", false, Tags.HasName)
        {
        }

        /// <summary>
        /// Handles the attribute.
        /// </summary>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="owner">The owner of this instance.</param>
        /// <param name="syntax">The binding syntax.</param>
        public override void Process(
            string value,
            IOwnXmlNodeProcessor owner,
            IBindingConfigurationSyntax<object> syntax)
        {
            syntax.Named(value);
        }
    }
}