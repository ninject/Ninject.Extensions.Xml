// -------------------------------------------------------------------------------------------------
// <copyright file="IXmlNodeProcessor.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Processors
{
    /// <summary>
    /// Processes an xml node.
    /// </summary>
    public interface IXmlNodeProcessor : IHaveXmlNodeName
    {
        /// <summary>
        /// Specifies if the processor applies to the given owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns>True if the processor is a applicable processor for the specified owner.</returns>
        bool AppliesTo(IOwnXmlNodeProcessor owner);
    }
}