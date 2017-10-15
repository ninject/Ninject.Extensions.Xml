// -------------------------------------------------------------------------------------------------
// <copyright file="IOwnXmlNodeProcessor.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Processors
{
    using System.Collections.Generic;

    /// <summary>
    /// Specifies that the the class owns xml node processors.
    /// </summary>
    public interface IOwnXmlNodeProcessor : IHaveXmlNodeName
    {
        /// <summary>
        /// Gets the tags of the elements that apply to this owner as its children.
        /// </summary>
        /// <value>The tags of the elements that apply to this owner as its children.</value>
        IEnumerable<string> ElementTags { get; }
    }
}