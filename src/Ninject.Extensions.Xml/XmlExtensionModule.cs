// -------------------------------------------------------------------------------------------------
// <copyright file="XmlExtensionModule.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd. All rights reserved.
//   Copyright (c) 2009-2017 Ninject Project Contributors. All rights reserved.
//
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   You may not use this file except in compliance with one of the Licenses.
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