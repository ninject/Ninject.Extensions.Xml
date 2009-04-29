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
using System.Linq;
using System.Xml.Linq;
using Ninject.Components;
using Ninject.Extensions.Xml.Handlers;
using Ninject.Modules;
#endregion

namespace Ninject.Extensions.Xml
{
	public class XmlModuleLoaderPlugin : NinjectComponent, IModuleLoaderPlugin
	{
		private static readonly string[] Extensions = new[] { ".xml" };

		public IEnumerable<string> SupportedExtensions
		{
			get { return Extensions; }
		}

		public IKernel Kernel { get; private set; }
		public IDictionary<string, IXmlElementHandler> ElementHandlers { get; private set; }

		public XmlModuleLoaderPlugin(IKernel kernel, IEnumerable<IXmlElementHandler> elementHandlers)
		{
			Kernel = kernel;
			ElementHandlers = elementHandlers.ToDictionary(handler => handler.ElementName);
		}

		public void LoadModules(IEnumerable<string> filenames)
		{
			Kernel.Load(GetModules(filenames));
		}

		private IEnumerable<INinjectModule> GetModules(IEnumerable<string> filenames)
		{
			foreach (string filename in filenames)
			{
				var document = XDocument.Load(filename);

				foreach (XElement moduleElement in document.Elements("module"))
					yield return new XmlModule(moduleElement, ElementHandlers);
			}
		}
	}
}
