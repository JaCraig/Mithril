namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Used to indicate that the generator should do both a date and time picker
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DateAndTimeAttribute : Attribute
    {
    }
}