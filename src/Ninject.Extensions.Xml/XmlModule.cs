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
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using Ninject.Extensions.Xml.Handlers;
using Ninject.Modules;
#endregion

namespace Ninject.Extensions.Xml
{
	public class XmlModule : NinjectModule
	{
		private readonly string _name;

		public XElement ModuleElement { get; private set; }
		public IDictionary<string, IXmlElementHandler> ElementHandlers { get; private set; }

		public override string Name
		{
			get { return _name; }
		}

		public XmlModule(XElement moduleElement, IDictionary<string, IXmlElementHandler> elementHandlers)
		{
			ModuleElement = moduleElement;
			ElementHandlers = elementHandlers;

			XAttribute attribute = moduleElement.Attribute("name");

			if (attribute == null)
				throw new ConfigurationErrorsException("<module> element does not have a required 'name' attribute.");

			_name = attribute.Value;
		}

		public override void Load()
		{
			foreach (XElement child in ModuleElement.Elements())
			{
				string name = child.Name.LocalName;

				if (!ElementHandlers.ContainsKey(name))
					throw new ConfigurationErrorsException(String.Format("<module> element contains an unknown element type '{0}'.", name));

				ElementHandlers[name].Handle(this, child);
			}
		}
	}
}
