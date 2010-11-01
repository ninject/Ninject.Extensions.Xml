namespace Ninject.Extensions.Xml.Fakes
{
    using Ninject.Activation;

    public class WeaponProvider : Provider<IWeapon>
    {
        protected override IWeapon CreateInstance(IContext context)
        {
            return new Sword();
        }
    }
}
