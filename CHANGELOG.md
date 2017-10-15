# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [3.3.0-beta1] - 2017-10-15

### Added
 - Support .NET Standard 2.0
 - Can read configuration from app.config/web.config (.NET Framework only)

### Removed
 - .NET 3.5, .NET 4.0 and Silverlight

[3.0.0.0]
---------------
- Removed: No web builds. All builds are have not reference to System.Web anymore
- Changed: InRequestScope support requires the Ninject.Web.Common.Xml extension shipped with Ninject.Web.Common