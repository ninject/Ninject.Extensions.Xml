// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

namespace Ninject.Extensions.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Xml.Linq;
    using Ninject.Extensions.Xml.Handlers;
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
        /// Initializes a new instance of the <see cref="XmlModule"/> class.
        /// </summary>
        /// <param name="moduleElement">The module xml element.</param>
        /// <param name="elementHandlers">The element handlers.</param>
        public XmlModule(XElement moduleElement, IDictionary<string, IXmlElementHandler> elementHandlers)
        {
            this.ModuleElement = moduleElement;
            this.ElementHandlers = elementHandlers;

            XAttribute attribute = moduleElement.Attribute("name");

            if (attribute == null)
            {
                throw new ConfigurationErrorsException("<module> element does not have a required 'name' attribute.");
            }

            this.name = attribute.Value;
        }
        
        /// <summary>
        /// Gets the module xml element.
        /// </summary>
        /// <value>The module xml element.</value>
        public XElement ModuleElement { get; private set; }

        /// <summary>
        /// Gets the element handlers.
        /// </summary>
        /// <value>The element handlers.</value>
        public IDictionary<string, IXmlElementHandler> ElementHandlers { get; private set; }

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
            foreach (XElement child in this.ModuleElement.Elements())
            {
                string moduleName = child.Name.LocalName;

                if (!this.ElementHandlers.ContainsKey(moduleName))
                {
                    throw new ConfigurationErrorsException(String.Format("<module> element contains an unknown element type '{0}'.", moduleName));
                }

                this.ElementHandlers[moduleName].Handle(this, child);
            }
        }
    }
}
