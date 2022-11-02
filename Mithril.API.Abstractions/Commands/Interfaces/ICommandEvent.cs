﻿using Mithril.Data.Abstractions.Interfaces;
using System.Dynamic;

namespace Mithril.API.Abstractions.Commands.Interfaces
{
    /// <summary>
    /// Event interface
    /// </summary>
    /// <seealso cref="IModel"/>
    public interface ICommandEvent : IEquatable<ICommandEvent>, IModel
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Determines whether this instance can run.
        /// </summary>
        /// <returns><c>true</c> if this instance can run; otherwise, <c>false</c>.</returns>
        bool CanRun();

        /// <summary>
        /// Gets the data within the event.
        /// </summary>
        /// <returns>The data from the event.</returns>
        ExpandoObject GetData();

        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <returns>The data schema.</returns>
        string GetSchema();
    }
}