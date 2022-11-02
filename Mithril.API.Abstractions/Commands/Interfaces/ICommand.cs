using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.API.Abstractions.Commands.Interfaces
{
    /// <summary>
    /// Command interface
    /// </summary>
    public interface ICommand : IEquatable<ICommand>, IModel
    {
    }
}