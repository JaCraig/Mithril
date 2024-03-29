﻿using System.Reflection;

namespace Mithril.Admin.Abstractions.ExtensionMethods
{
    /// <summary>
    /// PropertyInfo extension methods
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Determines whether the specified property has attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns><c>true</c> if the specified property has attribute; otherwise, <c>false</c>.</returns>
        public static bool HasAttribute<TAttribute>(this PropertyInfo? property)
            where TAttribute : Attribute => property?.GetCustomAttribute<TAttribute>() is not null;
    }
}