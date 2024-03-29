﻿using Mithril.Core.Abstractions.Modules.Interfaces;
using System.Security.Claims;

namespace Mithril.API.Abstractions.Query.Interfaces
{
    /// <summary>
    /// Query interface
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        IArgument[] Arguments { get; }

        /// <summary>
        /// Gets the deprecation reason.
        /// </summary>
        /// <value>The deprecation reason.</value>
        string? DeprecationReason { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        string? Description { get; }

        /// <summary>
        /// Gets the features associated with this command.
        /// </summary>
        /// <value>The features associated with this command.</value>
        IFeature[] Features { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the type of the return.
        /// </summary>
        /// <value>The type of the return.</value>
        Type ReturnType { get; }

        /// <summary>
        /// Gets the version (not guaranteed to be used in all query providers, but defaults to "v1").
        /// </summary>
        /// <value>The version.</value>
        string? Version { get; }
    }

    /// <summary>
    /// Query interface
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    public interface IQuery<TClass> : IQuery
        where TClass : class
    {
        /// <summary>
        /// Used to resolve the data asked for by the query.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The data specified.</returns>
        Task<TClass?> ResolveAsync(ClaimsPrincipal? user, Arguments arguments);
    }
}