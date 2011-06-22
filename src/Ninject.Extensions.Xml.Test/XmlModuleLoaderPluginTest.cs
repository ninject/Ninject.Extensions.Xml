namespace Ninject.Extensions.Xml
{
    using System.Linq;

    using FluentAssertions;

    using Ninject.Extensions.Xml.Fakes;
    using Xunit;

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
            kernel.HasModule("basicTest").Should().BeTrue();
            kernel.HasModule("providerTest").Should().BeTrue();
            kernel.HasModule("metadataTest").Should().BeTrue();
        }

        [Fact]
        public void CanActivateServicesDefinedInModules()
        {
            var weapons = kernel.GetAll<IWeapon>().ToList();
            weapons.Count.Should().Be(5);
        }

        [Fact]
        public void CanActivateServiceByName()
        {
            var weapon1 = kernel.Get<IWeapon>("melee");
            weapon1.Should().NotBeNull();
            weapon1.Should().BeOfType<Sword>();

            var weapon2 = kernel.Get<IWeapon>("range");
            weapon2.Should().NotBeNull();
            weapon2.Should().BeOfType<Shuriken>();
        }

        [Fact]
        public void CanActivateServiceByMetadata()
        {
            var ironWeapons = kernel.GetAll<IWeapon>(w => w.Get<string>("metal") == "iron").ToList();
            ironWeapons.Count.Should().Be(2);
            ironWeapons[0].Should().BeOfType<Sword>();
            ironWeapons[1].Should().BeOfType<Shuriken>();
        }
    }
}
