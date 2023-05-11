using Mithril.Background.Abstractions.Frequencies;
using Mithril.Tests.Helpers;

namespace Mithril.Background.Abstractions.Tests.Frequencies
{
    /// <summary>
    /// RunOnce tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RunOnce&gt;" />
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
            RunOnce RunOnce = new RunOnce();
            RunOnce.CanRun(DateTime.Now, DateTime.Now);
            DateTime LastRunTime = DateTime.Now;
            DateTime CurrentTime = DateTime.Now;

            bool CanRun = RunOnce.CanRun(LastRunTime, CurrentTime);

            Assert.False(CanRun);
        }

        /// <summary>
        /// Determines whether this instance [can run returns true when has not run before].
        /// </summary>
        [Fact]
        public void CanRun_ReturnsTrue_WhenHasNotRunBefore()
        {
            RunOnce RunOnce = new RunOnce();
            DateTime LastRunTime = DateTime.Now;
            DateTime CurrentTime = DateTime.Now;

            bool CanRun = RunOnce.CanRun(LastRunTime, CurrentTime);

            Assert.True(CanRun);
        }
    }
}