using System;
using Ninject.Activation;

namespace Ninject.Extensions.Xml.Tests.Fakes
{
	public class WeaponProvider : Provider<IWeapon>
	{
		protected override IWeapon CreateInstance(IContext context)
		{
			return new Sword();
		}
	}
}
