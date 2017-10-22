// -------------------------------------------------------------------------------------------------
// <copyright file="IBindingBuilderFactory.cs" company="Ninject Project Contributors">
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

namespace Ninject.Extensions.Xml.Processors
{
    using System.Xml.Linq;

    using Ninject.Components;
    using Ninject.Planning.Bindings;
    using Ninject.Syntax;

    /// <summary>
    /// Factory for the binding builder.
    /// </summary>
    public interface IBindingBuilderFactory : INinjectComponent
    {
        /// <summary>
        /// Creates a new binding builder and returns its binding syntax.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="module">The module.</param>
        /// <returns>The binding syntax of the created binding builder.</returns>
        IBindingConfigurationSyntax<object> Create(XElement element, IBindingRoot module);
    }
}