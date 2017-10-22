// -------------------------------------------------------------------------------------------------
// <copyright file="IXmlNodeProcessor.cs" company="Ninject Project Contributors">
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
    /// <summary>
    /// Processes an xml node.
    /// </summary>
    public interface IXmlNodeProcessor : IHaveXmlNodeName
    {
        /// <summary>
        /// Specifies if the processor applies to the given owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns>True if the processor is a applicable processor for the specified owner.</returns>
        bool AppliesTo(IOwnXmlNodeProcessor owner);
    }
}