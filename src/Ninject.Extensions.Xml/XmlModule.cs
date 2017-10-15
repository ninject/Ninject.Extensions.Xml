// -------------------------------------------------------------------------------------------------
// <copyright file="XmlModule.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    using Ninject.Extensions.Xml.Extensions;
    using Ninject.Extensions.Xml.Processors;
    using Ninject.Modules;

    /// <summary>
    /// Ninject module used for the modules in the xml configurations.
    /// </summary>
    public class XmlModule : NinjectModule
    {
        /// <summary>
        /// The name of the module.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The module xml element.
        /// </summary>
        private readonly XElement moduleElement;

        /// <summary>
        /// Gets the element processors.
        /// </summary>
        /// <value>The element processors.</value>
        private readonly IDictionary<string, IModuleChildXmlElementProcessor> elementProcessors;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlModule"/> class.
        /// </summary>
        /// <param name="moduleElement">The module xml element.</param>
        /// <param name="elementProcessors">The element processors.</param>
        public XmlModule(XElement moduleElement, IDictionary<string, IModuleChildXmlElementProcessor> elementProcessors)
        {
            this.moduleElement = moduleElement;
            this.elementProcessors = elementProcessors;
            this.name = moduleElement.RequiredAttribute("name").Value;
        }

        /// <summary>
        /// Gets the module's name. Only a single module with a given name can be loaded at one time.
        /// </summary>
        /// <value>The name of the module.</value>
        public override string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            foreach (var child in this.moduleElement.Elements())
            {
                var processor = this.elementProcessors.GetProcessor(child.Name.LocalName, "module");
                processor.Handle(this, child);
            }
        }
    }
}