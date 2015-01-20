//-------------------------------------------------------------------------------
// <copyright file="IXmlAttributeProcessor.cs" company="Ninject Project Contributors">
//   Copyright (c) 2009-2011 Ninject Project Contributors
//   Authors: Ilya Verbitskiy (iverbitskiy@gmail.com)
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

#if !SILVERLIGHT && !WINDOWS_PHONE && !WINRT

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using Ninject.Extensions.Xml;
using Ninject.Extensions.Xml.Processors;
using Ninject.Modules;

namespace Ninject
{
    /// <summary>
    /// Extension methods for IKernel
    /// </summary>
    public static class ExtensionsForKernel
    {
        /// <summary>
        /// Load Ninject modules from Application Configuration File.
        /// </summary>
        /// <param name="kernel">Ninject kernel.</param>
        /// <param name="sectionName">Application Configuration File section name with modules definition.</param>
        public static void LoadFromConfiguration(this IKernel kernel, string sectionName)
        {
            var ninjectSection = (XDocument)ConfigurationManager.GetSection(sectionName);
            var modules = GetModules(kernel, ninjectSection);
            kernel.Load(modules);
        }

        /// <summary>
        /// Gets the modules.
        /// </summary>
        /// <param name="kernel">Ninject kernel.</param>
        /// <param name="document">XDocument with modules definition.</param>
        /// <returns>The modules found by this module loader.</returns>
        private static IEnumerable<INinjectModule> GetModules(IKernel kernel, XDocument document)
        {
            var elementProcessors = kernel.Components.GetAll<IModuleChildXmlElementProcessor>()
                                          .ToDictionary(processor => processor.XmlNodeName);

            return (from moduleElement in document.Root.Elements("module")
                    select new XmlModule(moduleElement, elementProcessors)).Cast<INinjectModule>();
        }
    }
}

#endif