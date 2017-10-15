// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataXmlElementProcessor.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Processors
{
    using System.Xml.Linq;

    using Ninject.Extensions.Xml.Extensions;
    using Ninject.Planning.Bindings;

    /// <summary>
    /// Processor for metadata elements.
    /// </summary>
    public class MetadataXmlElementProcessor : AbstractXmlElementProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataXmlElementProcessor"/> class.
        /// </summary>
        public MetadataXmlElementProcessor()
            : base("metadata", new string[0], Tags.HasMetadata)
        {
        }

        /// <summary>
        /// Processes the specified element.
        /// </summary>
        /// <param name="element">The element that shall be processed.</param>
        /// <param name="owner">The owner of this instance.</param>
        /// <param name="syntax">The binding syntax.</param>
        public override void Process(
            XElement element,
            IOwnXmlNodeProcessor owner,
            IBindingConfigurationSyntax<object> syntax)
        {
            XAttribute keyAttribute = element.RequiredAttribute("key");
            XAttribute valueAttribute = element.RequiredAttribute("value");

            syntax.WithMetadata(keyAttribute.Value, valueAttribute.Value);
        }
    }
}