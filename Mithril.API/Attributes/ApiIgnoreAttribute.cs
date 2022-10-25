namespace Mithril.API.Attributes
{
    /// <summary>
    /// Used to signify that a property should be ignored by the API.
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public class ApiIgnoreAttribute : Attribute
    {
    }
}