// -------------------------------------------------------------------------------------------------
// <copyright file="NinjectSectionHandler.cs" company="Ninject Project Contributors">
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

#if !NETSTANDARD2_0
namespace Ninject.Extensions.Xml.Configuration
{
    using System.Configuration;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// Handles configuration sections that contains Ninject modules definition.
    /// </summary>
    public class NinjectSectionHandler : ConfigurationSection
    {
        /// <summary>
        /// Gets the Ninject modules configuration.
        /// </summary>
        public XDocument NinjectModules
        {
            get;
            private set;
        }

        /// <summary>
        /// Reads XML from the configuration file.
        /// </summary>
        /// <param name="reader">The XmlReader that reads from the configuration file.</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            this.NinjectModules = XDocument.Load(reader);
        }
    }
}
#endif