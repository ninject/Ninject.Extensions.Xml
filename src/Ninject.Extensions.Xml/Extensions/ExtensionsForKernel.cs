// -------------------------------------------------------------------------------------------------
// <copyright file="ExtensionsForKernel.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

#if !NETSTANDARD2_0
namespace Ninject
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Xml.Linq;
    using Ninject.Extensions.Xml;
    using Ninject.Extensions.Xml.Configuration;
    using Ninject.Extensions.Xml.Processors;
    using Ninject.Modules;

    /// <summary>
    /// Extension methods for IKernel
    /// </summary>
    public static class ExtensionsForKernel
    {
        private const string SectionName = "ninject";

        /// <summary>
        /// Load Ninject modules from Application Configuration File.
        /// </summary>
        /// <param name="kernel">Ninject kernel.</param>
        public static void LoadFromConfiguration(this IKernel kernel)
        {
            var ninjectSection = (NinjectSectionHandler)ConfigurationManager.GetSection(SectionName);
            if (ninjectSection == null)
            {
                var message = string.Format("{0} configuration section is not found.", SectionName);
                throw new ConfigurationErrorsException(message);
            }

            var modules = GetModules(kernel, ninjectSection.NinjectModules);
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