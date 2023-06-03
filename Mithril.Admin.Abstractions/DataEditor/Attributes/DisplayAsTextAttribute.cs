namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Used to indicate that the generator should display this as text (may have the ability for markdown, html, etc. depending on the generator)
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DisplayAsTextAttribute : Attribute
    {
    }
}