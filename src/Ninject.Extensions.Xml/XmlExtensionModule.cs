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
using Ninject.Extensions.Xml.Handlers;
using Ninject.Modules;
#endregion

namespace Ninject.Extensions.Xml
{
	public class XmlExtensionModule : NinjectModule
	{
		public override string Name
		{
			get { return "Ninject.Extensions.Xml"; }
		}

		public override void Load()
		{
			Kernel.Components.Add<IXmlElementHandler, BindElementHandler>();
			Kernel.Components.Add<IModuleLoaderPlugin, XmlModuleLoaderPlugin>();
		}
	}
}
