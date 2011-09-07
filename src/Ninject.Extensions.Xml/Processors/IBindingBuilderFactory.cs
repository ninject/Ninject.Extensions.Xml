namespace Ninject.Extensions.Xml.Processors
{
    using System.Xml.Linq;

    using Ninject.Components;
    using Ninject.Planning.Bindings;
    using Ninject.Syntax;

    public interface IBindingBuilderFactory : INinjectComponent
    {
        IBindingSyntax<object> Create(XElement element, IBindingRoot module);
    }
}