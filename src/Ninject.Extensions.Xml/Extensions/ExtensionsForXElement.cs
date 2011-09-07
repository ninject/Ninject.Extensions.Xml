//-------------------------------------------------------------------------------
// <copyright file="ExtensionsForXElement.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009, Enkari, Ltd.
//   Copyright (c) 2009-2011 Ninject Project Contributors
//   Authors: Nate Kohari (nate@enkari.com)
//            Remo Gloor (remo.gloor@gmail.com)
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

namespace Ninject.Extensions.Xml.Extensions
{
    using System.Configuration;
    using System.Xml.Linq;

    /// <summary>
    /// Extension methods for <see cref="XElement"/>
    /// </summary>
    public static class ExtensionsForXElement
    {
        /// <summary>
        /// Gets a required attribute from an <see cref="XElement"/>.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The attribute</returns>
        /// <exception cref="ConfigurationErrorsException">A required attribute is missing.</exception>
        public static XAttribute RequiredAttribute(this XElement element, XName name)
        {
            XAttribute attribute = element.Attribute(name);

            if (attribute == null)
            {
                throw new ConfigurationErrorsException(
                    string.Format("The '{0}' element does not have the required attribute '{1}'.", element.Name, name.LocalName));
            }

            return attribute;
        }
    }
}