using Microsoft.FeatureManagement;
using Mithril.Communication.Email.Utils;
using Mithril.Tests.Helpers;
using System.Net.Sockets;

namespace Mithril.Communication.Email.Tests.Utils
{
    /// <summary>
    /// Email sender tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EmailSender&gt;"/>
    public class EmailSenderTests : TestBaseClass<EmailSender>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSenderTests"/> class.
        /// </summary>
        public EmailSenderTests()
        {
            TestObject = new EmailSender(new DummyFeatures(), null);
            ExceptionsToIgnore = new[]
            {
                typeof(NotImplementedException),
                typeof(ArgumentOutOfRangeException),
                typeof(ArgumentException),
                typeof(FormatException),
                typeof(ObjectDisposedException),
                typeof(EndOfStreamException),
                typeof(OutOfMemoryException),
                typeof(SocketException)
            };
        }

        protected class AsyncEnumerable : IAsyncEnumerable<string>
        {
            public IAsyncEnumerator<string> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                return new AsyncEnumerator();
            }
        }

        protected class AsyncEnumerator : IAsyncEnumerator<string>
        {
            public string Current { get; } = "";

            public ValueTask DisposeAsync()
            { return ValueTask.CompletedTask; }

            public ValueTask<bool> MoveNextAsync()
            { return ValueTask.FromResult(false); }
        }

        protected class DummyFeatures : IFeatureManager
        {
            public IAsyncEnumerable<string> GetFeatureNamesAsync()
            {
                return new AsyncEnumerable();
            }

            public Task<bool> IsEnabledAsync(string feature)
            { return Task.FromResult(false); }

            public Task<bool> IsEnabledAsync<TContext>(string feature, TContext context)
            {
                return Task.FromResult(false);
            }
        }
    }
}