namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Password attribute
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class PasswordAttribute : Attribute
    {
    }
}