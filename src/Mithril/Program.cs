using Mithril.Core.Extensions;
using Serilog;

namespace Mithril
{
    /// <summary>
    /// Main program
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static async Task Main(string[] args)
        {
            try
            {
                WebApplicationBuilder? Builder = WebApplication.CreateBuilder(args);
                await (Builder.SetupMithril()?.RunAsync() ?? Task.CompletedTask).ConfigureAwait(false);
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}