using System;
using System.Linq;
using Ninject.Extensions.Xml.Tests.Fakes;
using Xunit;
using Xunit.Should;

namespace Ninject.Extensions.Xml.Tests.XmlModuleLoaderPluginTests
{
	public class XmlModuleLoaderPluginContext
	{
		protected readonly IKernel kernel;

		public XmlModuleLoaderPluginContext()
		{
			var settings = new NinjectSettings { LoadExtensions = false };
			kernel = new StandardKernel(settings, new XmlExtensionModule());
		}
	}

	public class WhenLoadIsCalled : XmlModuleLoaderPluginContext
	{
		public WhenLoadIsCalled()
		{
			kernel.Load("Cases/*.xml");
		}

		[Fact]
		public void ModulesAreLoaded()
		{
			kernel.HasModule("basicTest").ShouldBeTrue();
			kernel.HasModule("providerTest").ShouldBeTrue();
			kernel.HasModule("metadataTest").ShouldBeTrue();
		}

		[Fact]
		public void CanActivateServicesDefinedInModules()
		{
			var weapons = kernel.GetAll<IWeapon>().ToList();
			weapons.Count.ShouldBe(5);
		}

		[Fact]
		public void CanActivateServiceByName()
		{
			var weapon1 = kernel.Get<IWeapon>("melee");
			weapon1.ShouldNotBeNull();
			weapon1.ShouldBeInstanceOf<Sword>();

			var weapon2 = kernel.Get<IWeapon>("range");
			weapon2.ShouldNotBeNull();
			weapon2.ShouldBeInstanceOf<Shuriken>();
		}

		[Fact]
		public void CanActivateServiceByMetadata()
		{
			var ironWeapons = kernel.GetAll<IWeapon>(w => w.Get<string>("metal") == "iron").ToList();
			ironWeapons.Count.ShouldBe(2);
			ironWeapons[0].ShouldBeInstanceOf<Sword>();
			ironWeapons[1].ShouldBeInstanceOf<Shuriken>();
		}
	}
}
