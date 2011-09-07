//-------------------------------------------------------------------------------
// <copyright file="XmlModuleLoaderPlugin.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009, Enkari, Ltd.
//   Copyright (c) 2009-2011 Ninject Project Contributors
//   Authors: Nate Kohari (nate@enkari.com)
//           
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   you may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Ninject.Components;
    using Ninject.Extensions.Xml.Processors;
    using Ninject.Modules;

    /// <summary>
    /// Module loader for modules defined in xml.
    /// </summary>
    public class XmlModuleLoaderPlugin : NinjectComponent, IModuleLoaderPlugin
    {
        /// <summary>
        /// The extensions processed by this module loader.
        /// </summary>
        private static readonly string[] Extensions = new[] { ".xml" };

        /// <summary>
        /// The ninject kernel.
        /// </summary>
        /// <value>The kernel.</value>
        private readonly IKernel kernel;

        /// <summary>
        /// Gets the xml element processors.
        /// </summary>
        /// <value>The xml element processors.</value>
        private readonly IDictionary<string, IModuleChildXmlElementProcessor> elementProcessors;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlModuleLoaderPlugin"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <param name="elementProcessors">The element processors.</param>
        public XmlModuleLoaderPlugin(IKernel kernel, IEnumerable<IModuleChildXmlElementProcessor> elementProcessors)
        {
            this.kernel = kernel;
            this.elementProcessors = elementProcessors.ToDictionary(processor => processor.XmlNodeName);
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
        /// Loads modules from the specified files.
        /// </summary>
        /// <param name="filenames">The names of the files to load modules from.</param>
        public void LoadModules(IEnumerable<string> filenames)
        {
            this.kernel.Load(this.GetModules(filenames));
        }

        /// <summary>
        /// Gets the modules.
        /// </summary>
        /// <param name="filenames">The filenames to be processed.</param>
        /// <returns>The modules found by this module loader.</returns>
        private IEnumerable<INinjectModule> GetModules(IEnumerable<string> filenames)
        {
            return (from filename in filenames 
                    select XDocument.Load(filename) into document 
                        from moduleElement in document.Elements("module") 
                        select new XmlModule(moduleElement, this.elementProcessors)).Cast<INinjectModule>();
        }
    }
}
