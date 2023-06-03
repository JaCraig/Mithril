namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Do not list attribute
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DoNotListAttribute : Attribute
    {
    }
}