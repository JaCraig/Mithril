using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Tests.Extensions
{
    /// <summary>
    /// MVC Builder extensions
    /// </summary>
    /// <seealso cref="TestBaseClass"/>
    public class IMvcBuilderExtensionsTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; } = typeof(IMvcBuilderExtensions);

        /// <summary>
        /// Adds the CSP media type adds CSP media type to supported media types when input
        /// formatter is system text json input formatter.
        /// </summary>
        [Fact]
        public void AddCspMediaType_AddsCspMediaTypeToSupportedMediaTypes_WhenInputFormatterIsSystemTextJsonInputFormatter()
        {
            var Services = new ServiceCollection();
            var MvcBuilder = Services.AddControllers(options => options.InputFormatters.Add(new SystemTextJsonInputFormatter(new JsonOptions(), null!)));

            MvcBuilder.AddCspMediaType();

            var ServiceProvider = MvcBuilder.Services.BuildServiceProvider();
            var JsonInputFormatter = (SystemTextJsonInputFormatter?)ServiceProvider.GetRequiredService<IOptions<MvcOptions>>()?.Value.InputFormatters.FirstOrDefault(x => x is SystemTextJsonInputFormatter);
            Assert.Contains("application/csp-report", JsonInputFormatter?.SupportedMediaTypes ?? new MediaTypeCollection());
        }

        /// <summary>
        /// Adds the CSP media type does not add CSP media type to supported media types when input
        /// formatter is not system text json input formatter.
        /// </summary>
        [Fact]
        public void AddCspMediaType_DoesNotAddCspMediaTypeToSupportedMediaTypes_WhenInputFormatterIsNotSystemTextJsonInputFormatter()
        {
            var Services = new ServiceCollection();
            var MvcBuilder = Services.AddControllers(options => options.InputFormatters.Clear());

            MvcBuilder.AddCspMediaType();

            var ServiceProvider = MvcBuilder.Services.BuildServiceProvider();
            var JsonInputFormatter = (SystemTextJsonInputFormatter?)ServiceProvider.GetRequiredService<IOptions<MvcOptions>>()?.Value.InputFormatters.FirstOrDefault(x => x is SystemTextJsonInputFormatter);
            Assert.DoesNotContain("application/csp-report", JsonInputFormatter?.SupportedMediaTypes ?? new MediaTypeCollection());
        }

        /// <summary>
        /// Adds the CSP media type returns the IMVCBuilder instance.
        /// </summary>
        [Fact]
        public void AddCspMediaType_ReturnsTheIMvcBuilderInstance()
        {
            var Services = new ServiceCollection();
            var MvcBuilder = Services.AddControllers(options => options.InputFormatters.Add(new SystemTextJsonInputFormatter(new JsonOptions(), null!)));

            var Result = MvcBuilder.AddCspMediaType();

            Assert.Same(MvcBuilder, Result);
        }
    }
}