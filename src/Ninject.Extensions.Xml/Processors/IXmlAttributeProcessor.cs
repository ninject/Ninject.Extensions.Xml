//-------------------------------------------------------------------------------
// <copyright file="IXmlAttributeProcessor.cs" company="Ninject Project Contributors">
//   Copyright (c) 2009-2011 Ninject Project Contributors
//   Authors: Remo Gloor (remo.gloor@gmail.com)
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

namespace Ninject.Extensions.Xml.Processors
{
    using Ninject.Components;
    using Ninject.Planning.Bindings;

    /// <summary>
    /// Processor for a xml attribute.
    /// </summary>
    public interface IXmlAttributeProcessor : IXmlNodeProcessor, INinjectComponent
    {
        /// <summary>
        /// Gets a value indicating whether the attribute processed by this instance is required.
        /// </summary>
        /// <value><c>true</c> if the attribute processed by this instance is required.; otherwise, <c>false</c>.</value>
        bool Required { get; }

        /// <summary>
        /// Handles the attribute.
        /// </summary>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="owner">The owner of this instance.</param>
        /// <param name="syntax">The binding syntax.</param>
        void Process(string value, IOwnXmlNodeProcessor owner, IBindingSyntax<object> syntax);
    }
}