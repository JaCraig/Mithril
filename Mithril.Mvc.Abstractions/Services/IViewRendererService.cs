using Mithril.Mvc.Abstractions.Enums;

namespace Mithril.Mvc.Abstractions.Services
{
    public interface IViewRendererService
    {
        /// <summary>
        /// Renders the specified view.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="model">The model.</param>
        /// <param name="format">The format.</param>
        /// <returns>The rendered view.</returns>
        Task<byte[]> RenderAsync<TModel>(string name, TModel model, RenderOptions? options = null, RenderFormat format = RenderFormat.HTML);
    }
}