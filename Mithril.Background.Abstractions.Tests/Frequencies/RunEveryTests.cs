using Mecha.xUnit;
using Mithril.Background.Abstractions.Frequencies;
using Mithril.Tests.Helpers;

namespace Mithril.Background.Abstractions.Tests.Frequencies
{
    /// <summary>
    /// RunEvery tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RunEvery&gt;"/>
    public class RunEveryTests : TestBaseClass<RunEvery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunEveryTests"/> class.
        /// </summary>
        public RunEveryTests()
        {
            TestObject = new RunEvery(TimeSpan.Zero);
            ObjectType = typeof(RunEvery);
        }

        /// <summary>
        /// Determines whether this instance [can run returns false when not enough time has passed]
        /// the specified time span.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        [Property]
        public void CanRun_ReturnsFalse_WhenNotEnoughTimeHasPassed(TimeSpan timeSpan)
        {
            if (timeSpan < TimeSpan.FromSeconds(1))
                timeSpan = TimeSpan.FromSeconds(1);
            else if (timeSpan >= TimeSpan.FromDays(30))
                timeSpan = TimeSpan.FromDays(30);
            var RunEvery = new RunEvery(timeSpan);
            DateTime LastRunTime = DateTime.Now;
            DateTime CurrentTime = DateTime.Now;

            var canRun = RunEvery.CanRun(LastRunTime, CurrentTime);

            Assert.False(canRun);
        }

        /// <summary>
        /// Determines whether this instance [can run returns true when enough time has passed] the
        /// specified time span.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        [Property]
        public void CanRun_ReturnsTrue_WhenEnoughTimeHasPassed(TimeSpan timeSpan)
        {
            if (timeSpan < TimeSpan.Zero)
                timeSpan = TimeSpan.Zero;
            else if (timeSpan >= TimeSpan.FromDays(30))
                timeSpan = TimeSpan.FromDays(30);
            var RunEvery = new RunEvery(timeSpan);
            DateTime LastRunTime = DateTime.Now.Subtract(timeSpan);
            DateTime CurrentTime = DateTime.Now;

            var canRun = RunEvery.CanRun(LastRunTime, CurrentTime);

            Assert.True(canRun);
        }
    }
}