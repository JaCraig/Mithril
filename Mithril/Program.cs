namespace Mithril
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
            await (builder.SetupMithril()?.RunAsync() ?? Task.CompletedTask).ConfigureAwait(false);
        }
    }
}