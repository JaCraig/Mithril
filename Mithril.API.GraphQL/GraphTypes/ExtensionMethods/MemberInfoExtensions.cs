using Antlr4.Runtime.Misc;
using BigBook;
using GraphQL;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.ExtensionMethods;
using System.Reflection;

namespace Mithril.API.GraphQL.GraphTypes.ExtensionMethods
{
    /// <summary>
    /// MemberInfo extension methods
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Gets the deprecation reason.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The deprecation reason</returns>
        public static string? GetDeprecationReason(this MemberInfo memberInfo)
        {
            if (memberInfo is null)
                return "";
            var DescriptionAttribute = memberInfo.GetCustomAttribute<ApiDepricationReasonAttribute>();
            if (DescriptionAttribute is null)
                return null;
            return DescriptionAttribute.DepricationReason;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The API description.</returns>
        public static string GetDescription(this MemberInfo memberInfo)
        {
            if (memberInfo is null)
                return "";
            var DescriptionAttribute = memberInfo.GetCustomAttribute<ApiDescriptionAttribute>();
            return string.IsNullOrEmpty(DescriptionAttribute?.Description) ?
                        $"Returns {memberInfo.Name.SplitCamelCase().ToLowerInvariant()} information." :
                        DescriptionAttribute.Description;
        }

        /// <summary>
        /// Gets the description for the property.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The description.</returns>
        public static string GetDescription(this PropertyInfo memberInfo)
        {
            if (memberInfo is null)
                return "";
            var DescriptionAttribute = memberInfo.GetCustomAttribute<ApiDescriptionAttribute>();
            return string.IsNullOrEmpty(DescriptionAttribute?.Description) ?
                        $"Returns {memberInfo.Name.SplitCamelCase().ToLowerInvariant()} information of type {memberInfo.PropertyType.Name}." :
                        DescriptionAttribute.Description;
        }

        /// <summary>
        /// Gets the description for the method.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The description.</returns>
        public static string GetDescription(this MethodInfo memberInfo)
        {
            if (memberInfo is null)
                return "";
            var DescriptionAttribute = memberInfo.GetCustomAttribute<ApiDescriptionAttribute>();
            if (string.IsNullOrEmpty(DescriptionAttribute?.Description))
            {
                var Result = $"Returns {memberInfo.Name.SplitCamelCase().ToLowerInvariant()} information of type {memberInfo.ReturnType.Name}";
                if (memberInfo.GetParameters().Length > 0)
                    Result += $" using the following arguments ({memberInfo.GetParameters().ToString(x => (x.Name?.ToCamelCase() ?? "") + (x.HasDefaultValue ? (" = " + x.DefaultValue) : "") ?? "", ", ")})";
                return Result + ".";
            }
            return DescriptionAttribute.Description;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The API description.</returns>
        public static string GetDescription(this Type memberInfo)
        {
            if (memberInfo is null)
                return "";
            var DescriptionAttribute = memberInfo.GetCustomAttribute<ApiDescriptionAttribute>();
            return string.IsNullOrEmpty(DescriptionAttribute?.Description) ?
                        $"{memberInfo.Name.SplitCamelCase().ToString(StringCase.FirstCharacterUpperCase)} object type." :
                        DescriptionAttribute.Description;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The name of the MemberInfo</returns>
        public static string GetName(this MemberInfo memberInfo)
        {
            if (memberInfo is null)
                return "";
            return new string(new char[] { memberInfo.Name[0] }).ToLower() + memberInfo.Name.Right(memberInfo.Name.Length - 1);
        }
    }
}