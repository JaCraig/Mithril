using DocumentFormat.OpenXml.Wordprocessing;
using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Services;
using Mithril.Routing.Abstractions.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace Mithril.Routing.Models
{
    /// <summary>
    /// Route entry
    /// </summary>
    /// <seealso cref="ModelBase&lt;RouteEntry&gt;"/>
    /// <seealso cref="IRoute"/>
    public class RouteEntry : ModelBase<RouteEntry>, IRoute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteEntry"/> class.
        /// </summary>
        public RouteEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteEntry"/> class.
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <param name="outputPath">The output path.</param>
        /// <exception cref="ArgumentNullException">inputPath or outputPath</exception>
        /// <exception cref="ArgumentException">inputPath or outputPath</exception>
        public RouteEntry(string inputPath, string outputPath)
        {
            if (string.IsNullOrEmpty(inputPath))
                throw new ArgumentNullException(nameof(inputPath));
            if (inputPath.Length > 1024)
                throw new ArgumentException(nameof(inputPath) + " is too long. Max of 1024 characters allowed.");
            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentNullException(nameof(outputPath));
            if (outputPath.Length > 1024)
                throw new ArgumentException(nameof(outputPath) + " is too long. Max of 1024 characters allowed.");
            InputPath = WebUtility.UrlDecode(inputPath);
            OutputPath = outputPath;
        }

        /// <summary>
        /// Gets or sets the input path.
        /// </summary>
        /// <value>The input path.</value>
        [MaxLength(1024)]
        public string? InputPath { get; set; }

        /// <summary>
        /// Gets or sets the output path.
        /// </summary>
        /// <value>The output path.</value>
        [MaxLength(1024)]
        public string? OutputPath { get; set; }

        /// <summary>
        /// Loads the route by input path.
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <param name="dataService">The data service.</param>
        /// <returns>The route specified.</returns>
        public static RouteEntry? Load(string? inputPath, IDataService? dataService)
        {
            if (string.IsNullOrEmpty(inputPath) || dataService is null)
                return null;
            inputPath = WebUtility.UrlDecode(inputPath);
            return Query(dataService)?.Where(x => x.InputPath == inputPath).FirstOrDefault();
        }

        /// <summary>
        /// Loads or creates the route.
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <param name="outputPath">The output path.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="user">The user.</param>
        /// <returns>The route entry.</returns>
        public static async Task<RouteEntry> LoadOrCreateAsync(string inputPath, string outputPath, IDataService dataService, ClaimsPrincipal? user)
        {
            var ReturnValue = Load(inputPath, dataService);
            if (ReturnValue is null)
            {
                ReturnValue = new RouteEntry(inputPath, outputPath);
                if (dataService is not null)
                    await dataService.SaveAsync(user, ReturnValue).ConfigureAwait(false);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(RouteEntry left, RouteEntry right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(RouteEntry left, RouteEntry right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(RouteEntry left, RouteEntry right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(RouteEntry first, RouteEntry second)
        {
            return ReferenceEquals(first, second)
                || (first is not null
                    && second is not null
                    && first.CompareTo(second) == 0);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(RouteEntry left, RouteEntry right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(RouteEntry left, RouteEntry right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(RouteEntry? other)
        {
            return base.CompareTo(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(RouteEntry other)
        {
            return base.Equals(other);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString() => InputPath + " -> " + OutputPath;
    }
}