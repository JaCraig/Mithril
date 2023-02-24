using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Mithril.Routing.Abstractions.Services;

namespace Mithril.Routing.Transformers
{
    /// <summary>
    /// Route transformer
    /// </summary>
    /// <seealso cref="DynamicRouteValueTransformer"/>
    public class RouteTransformer : DynamicRouteValueTransformer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteTransformer"/> class.
        /// </summary>
        /// <param name="routeManager">The route manager.</param>
        public RouteTransformer(IRouteService routeManager)
        {
            RouteManager = routeManager;
        }

        /// <summary>
        /// Gets the route manager.
        /// </summary>
        /// <value>The route manager.</value>
        public IRouteService RouteManager { get; }

        /// <summary>
        /// Creates a set of transformed route values that will be used to select an action.
        /// </summary>
        /// <param name="httpContext">
        /// The <see cref="HttpContext"/> associated with the current request.
        /// </param>
        /// <param name="values">
        /// The route values associated with the current match. Implementations should not modify
        /// <paramref name="values"/>.
        /// </param>
        /// <returns>A task which asynchronously returns a set of route values.</returns>
        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            if (values?.ContainsKey("slug") != true || httpContext is null)
                return new ValueTask<RouteValueDictionary>(values!);
            var RequestPath = httpContext.Request.Path.Value;

            var TempRoute = RouteManager.GetRoute(RequestPath);

            values["slug"] = null;

            if (TempRoute is null || string.IsNullOrEmpty(TempRoute.OutputPath))
                return new ValueTask<RouteValueDictionary>(values);

            var Parts = TempRoute.OutputPath.Split("/", StringSplitOptions.RemoveEmptyEntries);
            switch (Parts.Length)
            {
                case 4:
                    values.Add("area", Parts[0]);
                    values.Add("controller", Parts[1]);
                    values.Add("action", Parts[2]);
                    values.Add("id", Parts[3]);
                    values["slug"] = null;
                    break;

                case 3:
                    values.Add("controller", Parts[0]);
                    values.Add("action", Parts[1]);
                    values.Add("id", Parts[2]);
                    values["slug"] = null;
                    break;

                case 2:
                    values.Add("controller", Parts[0]);
                    values.Add("action", Parts[1]);
                    values["slug"] = null;
                    break;

                case 1:
                    values.Add("controller", Parts[0]);
                    values.Add("action", "Index");
                    values["slug"] = null;
                    break;
            }

            return new ValueTask<RouteValueDictionary>(values);
        }
    }
}