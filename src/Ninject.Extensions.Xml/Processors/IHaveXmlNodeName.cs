// -------------------------------------------------------------------------------------------------
// <copyright file="IHaveXmlNodeName.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Processors
{
    /// <summary>
    /// Specifies that the class has a xml node name.
    /// </summary>
    public interface IHaveXmlNodeName
    {
        /// <summary>
        /// Gets the name of the XML node.
        /// </summary>
        /// <value>The name of the XML node.</value>
        string XmlNodeName { get; }
    }
}