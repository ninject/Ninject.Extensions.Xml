// -------------------------------------------------------------------------------------------------
// <copyright file="ExtensionsForDictionary.cs" company="Ninject Project Contributors">
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

namespace Ninject.Extensions.Xml.Extensions
{
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// Extension methods for IDictionary{string, TProcessor}
    /// </summary>
    public static class ExtensionsForDictionary
    {
        /// <summary>
        /// Gets the processor for the specified element.
        /// </summary>
        /// <typeparam name="TProcessor">The type of the processor.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="parentElementName">Name of the parent element.</param>
        /// <returns>The processor for the specified element.</returns>
        /// <exception cref="XmlException">No processor for the specified element.</exception>
        public static TProcessor GetProcessor<TProcessor>(this IDictionary<string, TProcessor> dictionary, string elementName, string parentElementName)
        {
            if (!dictionary.TryGetValue(elementName, out TProcessor processor))
            {
                var message = $"<{parentElementName}> element contains an unknown element type '{elementName}'.";
                throw new XmlException(message);
            }

            return processor;
        }
    }
}