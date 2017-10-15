// -------------------------------------------------------------------------------------------------
// <copyright file="XmlExtensionModule.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd.
//   Copyright (c) 2009-2017 Ninject Project Contributors
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml
{
    using Ninject.Extensions.Xml.Processors;
    using Ninject.Extensions.Xml.Scopes;
    using Ninject.Modules;

    /// <summary>
    /// Ninject module for the xml extension.
    /// </summary>
    public class XmlExtensionModule : NinjectModule
    {
        /// <summary>
        /// Gets the module's name. Only a single module with a given name can be loaded at one time.
        /// </summary>
        public override string Name
        {
            get { return "Ninject.Extensions.Xml"; }
        }

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Kernel.Components.Add<IModuleChildXmlElementProcessor, BindXmlElementProcessor>();
            this.Kernel.Components.Add<IModuleLoaderPlugin, XmlModuleLoaderPlugin>();

            this.Kernel.Components.Add<IXmlAttributeProcessor, NameXmlAttributeProcessor>();
            this.Kernel.Components.Add<IXmlAttributeProcessor, ScopeXmlAttributeProcessor>();
            this.Kernel.Components.Add<IXmlElementProcessor, MetadataXmlElementProcessor>();

            this.Kernel.Components.Add<IScopeHandler, SingletonScopeHandler>();
            this.Kernel.Components.Add<IScopeHandler, ThreadScopeHandler>();
            this.Kernel.Components.Add<IScopeHandler, TransientScopeHandler>();

            this.Kernel.Components.AddTransient<IChildElementProcessor, ChildElementProcessor>();
            this.Kernel.Components.Add<IBindingBuilderFactory, BindingBuilderFactory>();
        }
    }
}