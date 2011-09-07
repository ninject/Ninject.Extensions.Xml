#region License
// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 
#endregion
#region Using Directives
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
#endregion

#if !NETCF
[assembly: AllowPartiallyTrustedCallers]
#endif

[assembly: InternalsVisibleTo("Ninject.Extensions.Xml.Test,PublicKey=0024000004800000940000000602000000240000525341310004000001000100f3fc252fdcdfdba2e6d41c88aa5d644aa480c3776f4d7a3f02625347a53fef16b3940741285b67067480cc1eda51f1a9b255cc3af2dcf77325621bd9f644de9e1311a5d2f8bd3054573da970c33566033d91c0fe4420d5b01f996a32ae3a44fad49974edb8546f418eca586ea085a1a175a1b79d6ec84f75d4b814a40b2abcb9")]