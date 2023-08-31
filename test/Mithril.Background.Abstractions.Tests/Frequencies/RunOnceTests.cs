using Mithril.Background.Abstractions.Frequencies;
using Mithril.Tests.Helpers;

namespace Mithril.Background.Abstractions.Tests.Frequencies
{
    /// <summary>
    /// RunOnce tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RunOnce&gt;"/>
    public class RunOnceTests : TestBaseClass<RunOnce>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunOnceTests"/> class.
        /// </summary>
        public RunOnceTests()
        {
            TestObject = new RunOnce();
        }

        /// <summary>
        /// Determines whether this instance [can run returns false when has run before].
        /// </summary>
        [Fact]
        public void CanRun_ReturnsFalse_WhenHasRunBefore()
        {
            var RunOnce = new RunOnce();
            _ = RunOnce.CanRun(DateTime.Now, DateTime.Now);
            DateTime LastRunTime = DateTime.Now;
            DateTime CurrentTime = DateTime.Now;

            var CanRun = RunOnce.CanRun(LastRunTime, CurrentTime);

            Assert.False(CanRun);
        }

        /// <summary>
        /// Determines whether this instance [can run returns true when has not run before].
        /// </summary>
        [Fact]
        public void CanRun_ReturnsTrue_WhenHasNotRunBefore()
        {
            var RunOnce = new RunOnce();
            DateTime LastRunTime = DateTime.Now;
            DateTime CurrentTime = DateTime.Now;

            var CanRun = RunOnce.CanRun(LastRunTime, CurrentTime);

            Assert.True(CanRun);
        }
    }
}