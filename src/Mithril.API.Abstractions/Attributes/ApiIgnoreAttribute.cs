﻿namespace Mithril.API.Abstractions.Attributes
{
    /// <summary>
    /// Used to signify that a property should be ignored by the API.
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class)]
    public class ApiIgnoreAttribute : Attribute
    {
    }
}