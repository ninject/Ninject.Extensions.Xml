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
using Ninject.Planning.Bindings;
using Ninject.Syntax;
#endregion

namespace Ninject.Extensions.Xml.Handlers.Extensions
{
	public static class ExtensionsForXElement
	{
		public static XAttribute RequiredAttribute(this XElement element, XName name)
		{
			XAttribute attribute = element.Attribute(name);

			if (attribute == null)
				throw new ConfigurationErrorsException(String.Format("The '{0}' element does not have the required attribute '{1}'.", element.Name, name.LocalName));

			return attribute;
		}
	}
}