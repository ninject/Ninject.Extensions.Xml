// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScopeXmlAttributeProcessor.cs" company="Ninject Project Contributors">
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
// --------------------------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Processors
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using Ninject.Extensions.Xml.Scopes;
    using Ninject.Planning.Bindings;

    /// <summary>
    /// Processes the scope attribute.
    /// </summary>
    public class ScopeXmlAttributeProcessor : AbstractXmlAttributeProcessor
    {
        /// <summary>
        /// Maps the scope names to the processor that is responsible.
        /// </summary>
        private readonly Dictionary<string, IScopeHandler> scopeHandlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopeXmlAttributeProcessor"/> class.
        /// </summary>
        /// <param name="scopeHandlers">
        /// The scope processors.
        /// </param>
        public ScopeXmlAttributeProcessor(IEnumerable<IScopeHandler> scopeHandlers)
            : base("scope", false, Tags.HasScope)
        {
            this.scopeHandlers = scopeHandlers.ToDictionary(processor => processor.ScopeName);
        }

        /// <summary>
        /// Handles the attribute.
        /// </summary>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="owner">The owner of this instance.</param>
        /// <param name="syntax">The binding syntax.</param>
        public override void Process(
            string value, 
            IOwnXmlNodeProcessor owner, 
            IBindingSyntax<object> syntax)
        {
            IScopeHandler scopeHandler;
            if (this.scopeHandlers.TryGetValue(value, out scopeHandler))
            {
                scopeHandler.SetScope(syntax);
            }
            else
            {
                throw new ConfigurationErrorsException(this.GetInvalidScopeErrorMessage(value, owner.XmlNodeName));
            }
        }

        /// <summary>
        /// Gets the invalid scope error message.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="parentElementName">The name of the parent element.</param>
        /// <returns>The error message.</returns>
        private string GetInvalidScopeErrorMessage(string scope, string parentElementName)
        {
            string[] scopes = this.scopeHandlers.Keys.ToArray();
            string validScopes = string.Join(", ", scopes, 0, scopes.Length - 1);
            string lastScope = scopes[scopes.Length - 1];
            return string.Format(
                "The '{3}' element has an unknown value '{0}' for its 'scope' attribute. Valid values are {1} and {2}.",
                scope,
                validScopes,
                lastScope,
                parentElementName);
        }
    }
}