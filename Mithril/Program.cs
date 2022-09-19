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
                WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
                await (builder.SetupMithril()?.RunAsync() ?? Task.CompletedTask).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}