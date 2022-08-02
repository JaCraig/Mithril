namespace Mithril.Core.Extensions
{
    /// <summary>
    /// Generic Extensions
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Whens the predicate is true, run the method.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="predicate">if set to <c>true</c> [predicate].</param>
        /// <param name="method">The method to run if true.</param>
        /// <returns>The builder.</returns>
        public static TObject When<TObject>(
            this TObject obj,
            bool predicate,
            Func<TObject, TObject> method) => method is not null && predicate ? method(obj) : obj;
    }
}