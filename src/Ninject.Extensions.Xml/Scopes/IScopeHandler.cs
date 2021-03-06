// -------------------------------------------------------------------------------------------------
// <copyright file="IScopeHandler.cs" company="Ninject Project Contributors">
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

namespace Ninject.Extensions.Xml.Scopes
{
    using Ninject.Components;
    using Ninject.Syntax;

    /// <summary>
    /// Processor for a specific scope type.
    /// </summary>
    public interface IScopeHandler : INinjectComponent
    {
        /// <summary>
        /// Gets the name of the scope processed by this instance.
        /// </summary>
        /// <value>The name of the scope processed by this instance.</value>
        string ScopeName { get; }

        /// <summary>
        /// Sets the scope using the given syntax.
        /// </summary>
        /// <param name="syntax">The syntax that is used to set the scope.</param>
        void SetScope(IBindingInSyntax<object> syntax);
    }
}