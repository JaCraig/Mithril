﻿using Microsoft.FeatureManagement;
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
            ExceptionsToIgnore =
            [
                typeof(NotImplementedException),
                typeof(ArgumentOutOfRangeException),
                typeof(ArgumentException),
                typeof(FormatException),
                typeof(ObjectDisposedException),
                typeof(EndOfStreamException),
                typeof(OutOfMemoryException),
                typeof(SocketException)
            ];
        }

        protected class AsyncEnumerable : IAsyncEnumerable<string>
        {
            public IAsyncEnumerator<string> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new AsyncEnumerator();
        }

        protected class AsyncEnumerator : IAsyncEnumerator<string>
        {
            public string Current { get; } = "";

            public ValueTask DisposeAsync() => ValueTask.CompletedTask;

            public ValueTask<bool> MoveNextAsync() => ValueTask.FromResult(false);
        }

        protected class DummyFeatures : IFeatureManager
        {
            public IAsyncEnumerable<string> GetFeatureNamesAsync() => new AsyncEnumerable();

            public Task<bool> IsEnabledAsync(string feature) => Task.FromResult(false);

            public Task<bool> IsEnabledAsync<TContext>(string feature, TContext context) => Task.FromResult(false);
        }
    }
}