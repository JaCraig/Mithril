namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Ignore attribute
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreAttribute : Attribute
    {
    }
}