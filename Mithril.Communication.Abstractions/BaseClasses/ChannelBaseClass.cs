using Mithril.Communication.Abstractions.Interfaces;

namespace Mithril.Communication.Abstractions.BaseClasses
{
    /// <summary>
    /// Communication channel base class
    /// </summary>
    /// <seealso cref="IChannel"/>
    public abstract class ChannelBaseClass : IChannel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelBaseClass"/> class.
        /// </summary>
        protected ChannelBaseClass()
        {
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public abstract string Name { get; }
    }
}