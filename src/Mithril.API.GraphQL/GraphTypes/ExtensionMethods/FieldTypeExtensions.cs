using GraphQL;
using GraphQL.Builders;
using GraphQL.Types;
using Mithril.API.Abstractions.Attributes;
using System.Reflection;

namespace Mithril.API.GraphQL.GraphTypes.ExtensionMethods
{
    /// <summary>
    /// FieldType extensions
    /// </summary>
    public static class FieldTypeExtensions
    {
        /// <summary>
        /// Sets the security.
        /// </summary>
        /// <param name="fieldBuilder">The field builder.</param>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The field builder</returns>
        public static FieldType? SetSecurity(this FieldType? fieldBuilder, MemberInfo? memberInfo)
        {
            if (fieldBuilder is null || memberInfo is null)
                return fieldBuilder;
            ApiAllowAnonymousAttribute? AnonymousAttribute = memberInfo.GetCustomAttribute<ApiAllowAnonymousAttribute>();
            if (AnonymousAttribute is not null)
            {
                _ = fieldBuilder.AllowAnonymous();
                return fieldBuilder;
            }
            ApiAuthorizeAttribute? AuthorizeAttribute = memberInfo.GetCustomAttribute<ApiAuthorizeAttribute>();
            if (AuthorizeAttribute is null)
                return fieldBuilder;
            _ = string.IsNullOrEmpty(AuthorizeAttribute.PolicyName) && string.IsNullOrEmpty(AuthorizeAttribute.Roles)
                ? fieldBuilder.Authorize()
                : !string.IsNullOrEmpty(AuthorizeAttribute.Roles)
                ? fieldBuilder.AuthorizeWithRoles(AuthorizeAttribute.Roles ?? "")
                : fieldBuilder.AuthorizeWithPolicy(AuthorizeAttribute.PolicyName ?? "");

            return fieldBuilder;
        }

        /// <summary>
        /// Sets the security.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <typeparam name="TReturn">The type of the return.</typeparam>
        /// <param name="fieldBuilder">The field builder.</param>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The field builder.</returns>
        public static FieldBuilder<TClass, TReturn>? SetSecurity<TClass, TReturn>(this FieldBuilder<TClass, TReturn>? fieldBuilder, MemberInfo? memberInfo)
        {
            _ = (fieldBuilder?.FieldType.SetSecurity(memberInfo));
            return fieldBuilder;
        }
    }
}