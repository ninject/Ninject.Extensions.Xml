//-------------------------------------------------------------------------------
// <copyright file="XmlModuleLoaderPluginTest.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009, Enkari, Ltd.
//   Copyright (c) 2009-2011 Ninject Project Contributors
//   Authors: Nate Kohari (nate@enkari.com)
//            Remo Gloor (remo.gloor@gmail.com)
//           
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   you may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Ninject.Extensions.Xml.Fakes;
    using Xunit;

    public class XmlModuleLoaderPluginTest
    {
        public class WhenLoadIsCalled : IDisposable
        {
            protected readonly IKernel kernel;

            public WhenLoadIsCalled()
            {
                var settings = new NinjectSettings { LoadExtensions = false };
                this.kernel = new StandardKernel(settings, new XmlExtensionModule());
                this.kernel.Load("Cases/*.xml");
            }

            public void Dispose()
            {
                this.kernel.Dispose();
            }
            
            [Fact]
            public void ModulesAreLoaded()
            {
                this.kernel.HasModule("basicTest").Should().BeTrue();
                this.kernel.HasModule("providerTest").Should().BeTrue();
                this.kernel.HasModule("metadataTest").Should().BeTrue();
            }

            [Fact]
            public void CanActivateServicesDefinedInModules()
            {
                var weapons = this.kernel.GetAll<IWeapon>().ToList();
                weapons.Count.Should().Be(5);
            }

            [Fact]
            public void CanActivateServiceByName()
            {
                var weapon1 = this.kernel.Get<IWeapon>("melee");
                weapon1.Should().NotBeNull();
                weapon1.Should().BeOfType<Sword>();

                var weapon2 = this.kernel.Get<IWeapon>("range");
                weapon2.Should().NotBeNull();
                weapon2.Should().BeOfType<Shuriken>();
            }

            [Fact]
            public void CanActivateServiceByMetadata()
            {
                var ironWeapons = this.kernel.GetAll<IWeapon>(w => w.Get<string>("metal") == "iron").ToList();
                ironWeapons.Count.Should().Be(2);
                ironWeapons[0].Should().BeOfType<Sword>();
                ironWeapons[1].Should().BeOfType<Shuriken>();
            }
        }
    }
}