//-------------------------------------------------------------------------------
// <copyright file="WhenLoadIsCalledForProviderModule.cs" company="Ninject Project Contributors">
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

namespace Ninject.Extensions.Xml.XmlModuleTests
{
    using System.Linq;
    using FluentAssertions;
    using Ninject.Extensions.Xml.Fakes;
    using Ninject.Planning.Bindings;
    using Xunit;

    public class WhenLoadIsCalledForProviderModule : XmlModuleContext
    {
        private readonly XmlModule module;

        public WhenLoadIsCalledForProviderModule()
        {
            this.Kernel.Load("Cases/provider.xml");
            this.module = this.Kernel.GetModules().OfType<XmlModule>().Single();
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
}