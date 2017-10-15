// -------------------------------------------------------------------------------------------------
// <copyright file="Tags.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Processors
{
    /// <summary>
    /// The tags of the processors in this extension.
    /// </summary>
    public static class Tags
    {
        /// <summary>
        /// Defines that the processor is for bindings.
        /// </summary>
        public const string Binding = "Binding";

        /// <summary>
        /// Defines that the binding can have a scope
        /// </summary>
        public const string HasScope = "HasScope";

        /// <summary>
        /// Defines the the binding can have meta data.
        /// </summary>
        public const string HasMetadata = "Metadata";

        /// <summary>
        /// Defines that the binding can have conditions.
        /// </summary>
        public const string HasCondition = "HasCondition";

        /// <summary>
        /// Defines that the binding can have a name.
        /// </summary>
        public const string HasName = "HasName";
    }
}