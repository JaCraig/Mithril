namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Read only attribute
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ReadOnlyAttribute : Attribute
    {
    }
}