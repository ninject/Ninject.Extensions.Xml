using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Ninject.Extensions.Xml.Handlers;
using Ninject.Extensions.Xml.Tests.Fakes;
using Ninject.Planning.Bindings;
using Xunit;
using Xunit.Should;

namespace Ninject.Extensions.Xml.Tests.XmlModuleTests
{
	public class XmlModuleContext
	{
		protected readonly IKernel kernel;
		protected readonly IDictionary<string, IXmlElementHandler> elementHandlers;

		public XmlModuleContext()
		{
			kernel = new StandardKernel();
			elementHandlers = new Dictionary<string, IXmlElementHandler> { { "bind", new BindElementHandler() } };
		}
	}

	public class WhenLoadIsCalledForBasicModule : XmlModuleContext
	{
		protected readonly XmlModule module;

		public WhenLoadIsCalledForBasicModule()
		{
			var document = XDocument.Load("Cases/basic.xml");
			module = new XmlModule(document.Element("module"), elementHandlers);
			module.OnLoad(kernel);
		}

		[Fact]
		public void ModuleIsNamedAppropriately()
		{
			module.Name.ShouldBe("basicTest");
		}

		[Fact]
		public void ModuleLoadsBindings()
		{
			var bindings = module.Bindings.ToList();
			bindings.Count.ShouldBe(2);

			bindings[0].Service.ShouldBe(typeof(IWeapon));
			bindings[0].Target.ShouldBe(BindingTarget.Type);

			bindings[1].Service.ShouldBe(typeof(IWeapon));
			bindings[1].Target.ShouldBe(BindingTarget.Type);
		}
	}

	public class WhenLoadIsCalledForProviderModule : XmlModuleContext
	{
		protected readonly XmlModule module;

		public WhenLoadIsCalledForProviderModule()
		{
			var document = XDocument.Load("Cases/provider.xml");
			module = new XmlModule(document.Element("module"), elementHandlers);
			module.OnLoad(kernel);
		}

		[Fact]
		public void ModuleIsNamedAppropriately()
		{
			module.Name.ShouldBe("providerTest");
		}

		[Fact]
		public void ModuleLoadsBindings()
		{
			var bindings = module.Bindings.ToList();
			bindings.Count.ShouldBe(1);

			bindings[0].Service.ShouldBe(typeof(IWeapon));
			bindings[0].Target.ShouldBe(BindingTarget.Provider);
		}
	}

	public class WhenLoadIsCalledForModuleWithBindingMetadata : XmlModuleContext
	{
		protected readonly XmlModule module;

		public WhenLoadIsCalledForModuleWithBindingMetadata()
		{
			var document = XDocument.Load("Cases/metadata.xml");
			module = new XmlModule(document.Element("module"), elementHandlers);
			module.OnLoad(kernel);
		}

		[Fact]
		public void ModuleLoadsBindingMetadata()
		{
			var bindings = module.Bindings.ToList();
			bindings.Count.ShouldBe(2);

			bindings[0].Service.ShouldBe(typeof(IWeapon));
			bindings[0].Metadata.Get<string>("metal").ShouldBe("iron");
			bindings[0].Metadata.Get<string>("class").ShouldBe("melee");

			bindings[1].Service.ShouldBe(typeof(IWeapon));
			bindings[1].Metadata.Get<string>("metal").ShouldBe("iron");
			bindings[1].Metadata.Get<string>("class").ShouldBe("range");
		}
	}
}
