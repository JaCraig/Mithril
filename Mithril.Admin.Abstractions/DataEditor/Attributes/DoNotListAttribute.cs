namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Do not list attribute
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DoNotListAttribute : Attribute
    {
    }
}