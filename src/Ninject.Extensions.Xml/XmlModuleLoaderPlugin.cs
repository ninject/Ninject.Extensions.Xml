// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

namespace Ninject.Extensions.Xml
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Ninject.Components;
    using Ninject.Extensions.Xml.Handlers;
    using Ninject.Modules;

    /// <summary>
    /// 
    /// </summary>
    public class XmlModuleLoaderPlugin : NinjectComponent, IModuleLoaderPlugin
    {
        /// <summary>
        /// The extensions handled by this module loader.
        /// </summary>
        private static readonly string[] Extensions = new[] { ".xml" };

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlModuleLoaderPlugin"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <param name="elementHandlers">The element handlers.</param>
        public XmlModuleLoaderPlugin(IKernel kernel, IEnumerable<IXmlElementHandler> elementHandlers)
        {
            this.Kernel = kernel;
            this.ElementHandlers = elementHandlers.ToDictionary(handler => handler.ElementName);
        }
        
        /// <summary>
        /// Gets the file extensions that the plugin understands how to load.
        /// </summary>
        /// <value>The file extensions supported by this loader.</value>
        public IEnumerable<string> SupportedExtensions
        {
            get { return Extensions; }
        }

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        /// <value>The kernel.</value>
        public IKernel Kernel { get; private set; }

        /// <summary>
        /// Gets the xml element handlers.
        /// </summary>
        /// <value>The xml element handlers.</value>
        public IDictionary<string, IXmlElementHandler> ElementHandlers { get; private set; }

        /// <summary>
        /// Loads modules from the specified files.
        /// </summary>
        /// <param name="filenames">The names of the files to load modules from.</param>
        public void LoadModules(IEnumerable<string> filenames)
        {
            this.Kernel.Load(this.GetModules(filenames));
        }

        /// <summary>
        /// Gets the modules.
        /// </summary>
        /// <param name="filenames">The filenames to be handled.</param>
        /// <returns>The modules found by this module loader.</returns>
        private IEnumerable<INinjectModule> GetModules(IEnumerable<string> filenames)
        {
            return (from filename in filenames 
                    select XDocument.Load(filename) into document 
                        from moduleElement in document.Elements("module") 
                        select new XmlModule(moduleElement, this.ElementHandlers)).Cast<INinjectModule>();
        }
    }
}
