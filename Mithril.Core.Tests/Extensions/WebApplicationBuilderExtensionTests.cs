﻿using Microsoft.AspNetCore.Builder;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Tests.Extensions
{
    public class WebApplicationBuilderExtensionTests : TestBaseClass
    {
        protected override Type? ObjectType { get; set; } = typeof(WebApplicationBuilderExtensions);
    }
}