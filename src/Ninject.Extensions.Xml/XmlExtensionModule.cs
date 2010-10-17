// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

namespace Ninject.Extensions.Xml
{
    using Ninject.Extensions.Xml.Handlers;
    using Ninject.Modules;

    /// <summary>
    /// Ninject module for the xml extension.
    /// </summary>
    public class XmlExtensionModule : NinjectModule
    {
        /// <summary>
        /// Gets the module's name. Only a single module with a given name can be loaded at one time.
        /// </summary>
        /// <value></value>
        public override string Name
        {
            get { return "Ninject.Extensions.Xml"; }
        }

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Kernel.Components.Add<IXmlElementHandler, BindElementHandler>();
            Kernel.Components.Add<IModuleLoaderPlugin, XmlModuleLoaderPlugin>();
        }
    }
}
