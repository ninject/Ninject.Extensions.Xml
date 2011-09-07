namespace Ninject.Extensions.Xml.Processors
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    using Ninject.Components;
    using Ninject.Planning.Bindings;

    public interface IChildElementProcessor : INinjectComponent
    {
        /// <summary>
        /// Processes the attributes of the given element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="syntax">The binding syntax.</param>
        void ProcessAttributes(
            XElement element,
            IBindingSyntax<object> syntax);

        void ProcessAttributes(XElement element, IBindingSyntax<object> syntax, IEnumerable<string> excludedAttributes);

        /// <summary>
        /// Processes the child elements.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="syntax">The syntax.</param>
        void ProcessChildElements(
            XElement element,
            IBindingSyntax<object> syntax);

        void SetOwner(IOwnXmlNodeProcessor owner);
    }
}