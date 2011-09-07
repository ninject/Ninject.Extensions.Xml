// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

namespace Ninject.Extensions.Xml.Processors
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    using Ninject.Components;
    using Ninject.Syntax;

    /// <summary>
    /// Processor for the "Bind" Element
    /// </summary>
    public class BindXmlElementProcessor : NinjectComponent, IOwnXmlNodeProcessor, IModuleChildXmlElementProcessor
    {
        private readonly IKernel kernel;
        private readonly IBindingBuilderFactory bindingBuilderFactory;
        private readonly IChildElementProcessor childElementProcessor;
        private readonly string[] excludedAttributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindXmlElementProcessor"/> class.
        /// </summary>
        /// <param name="kernel">The ninject kernel.</param>
        /// <param name="bindingBuilderFactory">The binding builder factory.</param>
        /// <param name="elementProcessors">The element processors.</param>
        /// <param name="attributeProcessors">The attribute processors.</param>
        public BindXmlElementProcessor(
            IKernel kernel,
            IBindingBuilderFactory bindingBuilderFactory,
            IChildElementProcessor childElementProcessor)
        {
            this.kernel = kernel;
            this.bindingBuilderFactory = bindingBuilderFactory;
            this.ElementTags = new[] { Tags.Binding, Tags.HasCondition, Tags.HasMetadata, Tags.HasName, Tags.HasScope };
            this.excludedAttributes = new[] { "service", "to", "toProvider" };
            this.childElementProcessor = childElementProcessor;
            this.childElementProcessor.SetOwner(this);
        }

        /// <summary>
        /// Gets the name of the XML node.
        /// </summary>
        /// <value>The name of the XML node.</value>
        public string XmlNodeName
        {
            get
            {
                return "bind";
            }
        }

        /// <summary>
        /// Gets the tags of the elements that apply to this owner as its children.
        /// </summary>
        /// <value>
        /// The tags of the elements that apply to this owner as its children.
        /// </value>
        public IEnumerable<string> ElementTags { get; private set; }
        
        /// <summary>
        /// Handles the XElement.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="element">The element.</param>
        public void Handle(IBindingRoot module, XElement element)
        {
            var builder = this.bindingBuilderFactory.Create(element, module);
            
            this.childElementProcessor.ProcessAttributes(element, builder, this.excludedAttributes);
            this.childElementProcessor.ProcessChildElements(element, builder);
        }
    }
}