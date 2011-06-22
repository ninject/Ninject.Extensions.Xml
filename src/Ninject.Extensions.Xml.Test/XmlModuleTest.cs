namespace Ninject.Extensions.Xml
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using FluentAssertions;

    using Ninject.Extensions.Xml.Handlers;
    using Ninject.Extensions.Xml.Fakes;
    using Ninject.Planning.Bindings;
    using Xunit;

    public class XmlModuleContext
    {
        protected readonly IKernel kernel;
        protected readonly IDictionary<string, IXmlElementHandler> elementHandlers;

        public XmlModuleContext()
        {
            kernel = new StandardKernel();
            elementHandlers = new Dictionary<string, IXmlElementHandler> { { "bind", new BindElementHandler(kernel) } };
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
            module.Name.Should().Be("basicTest");
        }

        [Fact]
        public void ModuleLoadsBindings()
        {
            var bindings = module.Bindings.ToList();
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
            var document = XDocument.Load("Cases/provider.xml");
            module = new XmlModule(document.Element("module"), elementHandlers);
            module.OnLoad(kernel);
        }

        [Fact]
        public void ModuleIsNamedAppropriately()
        {
            module.Name.Should().Be("providerTest");
        }

        [Fact]
        public void ModuleLoadsBindings()
        {
            var bindings = module.Bindings.ToList();
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
            var document = XDocument.Load("Cases/metadata.xml");
            module = new XmlModule(document.Element("module"), elementHandlers);
            module.OnLoad(kernel);
        }

        [Fact]
        public void ModuleLoadsBindingMetadata()
        {
            var bindings = module.Bindings.ToList();
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
