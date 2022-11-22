namespace Mithril.API.Abstractions.Attributes
{
    /// <summary>
    /// Allow anonymous access attribute
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class ApiAllowAnonymousAttribute : Attribute
    {
    }
}