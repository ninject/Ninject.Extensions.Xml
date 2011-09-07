namespace Ninject.Extensions.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Ninject.Extensions.Xml.Fakes;
    using Ninject.Extensions.Xml.Processors;
    using Ninject.Planning.Bindings;

    using Xunit;

    public class XmlModuleContext : IDisposable
    {
        protected readonly IKernel kernel;
        protected readonly IDictionary<string, IModuleChildXmlElementProcessor> elementProcessors;

        public XmlModuleContext()
        {
            var settings = new NinjectSettings { LoadExtensions = false };
            this.kernel = new StandardKernel(settings, new XmlExtensionModule());
            this.elementProcessors = 
                this.kernel.Components
                    .GetAll<IModuleChildXmlElementProcessor>()
                    .ToDictionary(p => p.XmlNodeName);
        }

        public void Dispose()
        {
            this.kernel.Dispose();
        }
    }

    public class WhenLoadIsCalledForBasicModule : XmlModuleContext
    {
        protected readonly XmlModule module;

        public WhenLoadIsCalledForBasicModule()
        {
            this.kernel.Load("Cases/basic.xml");
            this.module = this.kernel.GetModules().OfType<XmlModule>().Single();
        }
        
        [Fact]
        public void ModuleIsNamedAppropriately()
        {
            this.module.Name.Should().Be("basicTest");
        }

        [Fact]
        public void ModuleLoadsBindings()
        {
            var bindings = this.module.Bindings.ToList();
            bindings.Count.Should().Be(2);

            bindings[0].Service.Should().Be(typeof(IWeapon));
            bindings[0].Target.Should().Be(BindingTarget.Type);

            bindings[1].Service.Should().Be(typeof(IWeapon));
            bindings[1].Target.Should().Be(BindingTarget.Type);
        }
    }

    public class WhenLoadIsCalledForProviderModule : XmlModuleContext
    {
        protected readonly XmlModule module;

        public WhenLoadIsCalledForProviderModule()
        {
            this.kernel.Load("Cases/provider.xml");
            this.module = this.kernel.GetModules().OfType<XmlModule>().Single();
        }

        [Fact]
        public void ModuleIsNamedAppropriately()
        {
            this.module.Name.Should().Be("providerTest");
        }

        [Fact]
        public void ModuleLoadsBindings()
        {
            var bindings = this.module.Bindings.ToList();
            bindings.Count.Should().Be(1);

            bindings[0].Service.Should().Be(typeof(IWeapon));
            bindings[0].Target.Should().Be(BindingTarget.Provider);
        }
    }

    public class WhenLoadIsCalledForModuleWithBindingMetadata : XmlModuleContext
    {
        protected readonly XmlModule module;

        public WhenLoadIsCalledForModuleWithBindingMetadata()
        {
            this.kernel.Load("Cases/metadata.xml");
            this.module = this.kernel.GetModules().OfType<XmlModule>().Single();
        }

        [Fact]
        public void ModuleLoadsBindingMetadata()
        {
            var bindings = this.module.Bindings.ToList();
            bindings.Count.Should().Be(2);

            bindings[0].Service.Should().Be(typeof(IWeapon));
            bindings[0].Metadata.Get<string>("metal").Should().Be("iron");
            bindings[0].Metadata.Get<string>("class").Should().Be("melee");

            bindings[1].Service.Should().Be(typeof(IWeapon));
            bindings[1].Metadata.Get<string>("metal").Should().Be("iron");
            bindings[1].Metadata.Get<string>("class").Should().Be("range");
        }
    }
}
