namespace Mithril.API.Abstractions.Services
{
    /// <summary>
    /// Command service interface
    /// </summary>
    public interface ICommandService
    {
        /// <summary>
        /// Processes any new commands.
        /// </summary>
        /// <returns>The async task.</returns>
        Task ProcessAsync();
    }
}