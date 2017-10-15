# Ninject.Extensions.Xml

[![Build status](https://ci.appveyor.com/api/projects/status/im7q5v57row061kr?svg=true)](https://ci.appveyor.com/project/Ninject/ninject-extensions-xml)
[![codecov](https://codecov.io/gh/ninject/Ninject.Extensions.Xml/branch/master/graph/badge.svg)](https://codecov.io/gh/ninject/Ninject.Extensions.Xml)
[![NuGet Version](http://img.shields.io/nuget/v/Ninject.Extensions.Xml.svg?style=flat)](https://www.nuget.org/packages/Ninject.Extensions.Xml/) 
[![NuGet Downloads](http://img.shields.io/nuget/dt/Ninject.Extensions.Xml.svg?style=flat)](https://www.nuget.org/packages/Ninject.Extensions.Xml/)


This extension allows users to create Ninject modules using XML and load them at runtime. Because XML is not a
programming language, these modules are naturally not as powerful as those defined using code.

An example of the syntax:

```xml
<module name="SomeModule">
  <bind service="Game.IWeapon" to="Game.Sword"/>
  <bind service="Game.IWarrior" toProvider="Game.SamuraiProvider"/>
</module>
```