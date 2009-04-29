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
using System.Xml.Linq;
using Ninject.Components;
#endregion

namespace Ninject.Extensions.Xml.Handlers
{
	public interface IXmlElementHandler : INinjectComponent
	{
		string ElementName { get; }
		void Handle(XmlModule module, XElement element);
	}
}