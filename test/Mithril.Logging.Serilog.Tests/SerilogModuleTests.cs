﻿using Mithril.Tests.Helpers;

namespace Mithril.Logging.Serilog.Tests
{
    public class SerilogModuleTests : TestBaseClass<SerilogModule>
    {
        public SerilogModuleTests()
        {
            TestObject = new SerilogModule();
            ExceptionsToIgnore = [typeof(AggregateException), typeof(InvalidOperationException)];
            DiscoverInheritedMethods = true;
        }
    }
}