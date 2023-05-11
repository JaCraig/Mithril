using Mecha.xUnit;
using Mithril.Background.Abstractions.Frequencies;
using Mithril.Tests.Helpers;

namespace Mithril.Background.Abstractions.Tests.Frequencies
{
    /// <summary>
    /// RunEvery tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RunEvery&gt;" />
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

        [Property]
        public void CanRun_ReturnsFalse_WhenNotEnoughTimeHasPassed(TimeSpan timeSpan)
        {
            if (timeSpan < TimeSpan.FromSeconds(1))
                timeSpan = TimeSpan.FromSeconds(1);
            else if (timeSpan >= TimeSpan.FromDays(30))
                timeSpan = TimeSpan.FromDays(30);
            RunEvery RunEvery = new RunEvery(timeSpan);
            DateTime LastRunTime = DateTime.Now;
            DateTime CurrentTime = DateTime.Now;

            bool canRun = RunEvery.CanRun(LastRunTime, CurrentTime);

            Assert.False(canRun);
        }

        [Property]
        public void CanRun_ReturnsTrue_WhenEnoughTimeHasPassed(TimeSpan timeSpan)
        {
            if (timeSpan < TimeSpan.Zero)
                timeSpan = TimeSpan.Zero;
            else if (timeSpan >= TimeSpan.FromDays(30))
                timeSpan = TimeSpan.FromDays(30);
            RunEvery RunEvery = new RunEvery(timeSpan);
            DateTime LastRunTime = DateTime.Now.Subtract(timeSpan);
            DateTime CurrentTime = DateTime.Now;

            bool canRun = RunEvery.CanRun(LastRunTime, CurrentTime);

            Assert.True(canRun);
        }
    }
}