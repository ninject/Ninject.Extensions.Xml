#region License
// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 
#endregion
#region Using Directives
using System;
using System.Configuration;
using System.Xml.Linq;
using Ninject.Components;
using Ninject.Extensions.Xml.Handlers.Extensions;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
#endregion

namespace Ninject.Extensions.Xml.Handlers
{
	public class BindElementHandler : NinjectComponent, IXmlElementHandler
	{
		public string ElementName
		{
			get { return "bind"; }
		}

		public void Handle(XmlModule module, XElement element)
		{
			XAttribute serviceAttribute = element.RequiredAttribute("service");

			Type service = GetTypeFromAttributeValue(serviceAttribute);

			var binding = new Binding(service);
			var builder = new BindingBuilder<object>(binding);

			module.AddBinding(binding);

			if (!HandleTarget(element, builder))
				throw new ConfigurationErrorsException("The 'bind' element does not define either a 'to' or 'toProvider' attribute.");

			ReadName(element, builder);
			ReadMetadata(element, builder);
			ReadScope(element, builder);
		}

		private void ReadName(XElement element, BindingBuilder<object> builder)
		{
			var nameAttribute = element.Attribute("name");

			if (nameAttribute != null)
				builder.Named(nameAttribute.Value);
		}

		private void ReadMetadata(XElement element, BindingBuilder<object> builder)
		{
			foreach (XElement metadataElement in element.Elements("metadata"))
			{
				XAttribute keyAttribute = metadataElement.RequiredAttribute("key");
				XAttribute valueAttribute = metadataElement.RequiredAttribute("value");

				builder.WithMetadata(keyAttribute.Value, valueAttribute.Value);
			}
		}

		private void ReadScope(XElement element, BindingBuilder<object> builder)
		{
			XAttribute scopeAttribute = element.Attribute("scope");

			if (scopeAttribute == null || String.IsNullOrEmpty(scopeAttribute.Value))
			{
				builder.InTransientScope();
				return;
			}

			string value = scopeAttribute.Value.ToLower();

			switch (value)
			{
				case "transient":
					builder.InTransientScope();
					break;

				case "singleton":
					builder.InSingletonScope();
					break;

				case "thread":
					builder.InThreadScope();
					break;

				case "request":
					builder.InRequestScope();
					break;

				default:
					throw new ConfigurationErrorsException(String.Format("The 'bind' element has an unknown value '{0}' for its 'scope' attribute. Valid values are transient, singleton, thread, and request.", value));
			}
		}

		private bool HandleTarget(XElement element, BindingBuilder<object> builder)
		{
			if (TryHandleToAttribute(element, builder))
				return true;

			if (TryHandleToProviderAttribute(element, builder))
				return true;

			return false;
		}

		private bool TryHandleToAttribute(XElement element, BindingBuilder<object> builder)
		{
			XAttribute toAttribute = element.Attribute("to");

			if (toAttribute == null)
				return false;

			Type implementation = GetTypeFromAttributeValue(toAttribute);
			builder.To(implementation);

			return true;
		}

		private bool TryHandleToProviderAttribute(XElement element, BindingBuilder<object> builder)
		{
			XAttribute providerAttribute = element.Attribute("toProvider");

			if (providerAttribute == null)
				return false;

			Type provider = GetTypeFromAttributeValue(providerAttribute);
			builder.ToProvider(provider);

			return true;
		}

		private Type GetTypeFromAttributeValue(XAttribute attribute)
		{
			Type service = Type.GetType(attribute.Value, false);

			if (service == null)
				throw new ConfigurationErrorsException(String.Format("Couldn't resolve type '{0}' defined in '{1}' attribute.", attribute.Value, attribute.Name));

			return service;
		}
	}
}