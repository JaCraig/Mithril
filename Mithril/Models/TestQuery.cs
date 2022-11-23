using BigBook;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.Query;
using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.API.Abstractions.Query.Interfaces;
using System.Security.Claims;

namespace Mithril.Models
{
    /// <summary>
    /// Test query
    /// </summary>
    /// <seealso cref="QueryBaseClass&lt;TestVM&gt;"/>
    public class TestQuery : QueryBaseClass<TestVM>
    {
        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public override IArgument[] Arguments { get; } = new IArgument[] {
            new Argument<string> { DefaultValue = "A", Description = "Description", Name = "name" },
            new Argument<int> { DefaultValue = 1, Description = "Description", Name = "count" }
        };

        /// <summary>
        /// Resolves the asynchronous.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public override Task<TestVM?> ResolveAsync(ClaimsPrincipal? arg, Arguments arguments)
        {
            var List = new List<TestVM2>();
            arguments.GetValue<int>("Count").Times(x => List.Add(new TestVM2() { A = arguments.GetValue<string>("Name") }));
            return Task.FromResult<TestVM?>(new TestVM { A = List });
        }
    }

    /// <summary>
    /// Test VM
    /// </summary>
    public class TestVM
    {
        /// <summary>
        /// Gets or sets a.
        /// </summary>
        /// <value>a.</value>
        [ApiAuthorize("Test")]
        [ApiDescription("This will show up in the API")]
        public List<TestVM2> A { get; set; } = new List<TestVM2>();
    }

    /// <summary>
    /// TestVM 2
    /// </summary>
    public class TestVM2
    {
        /// <summary>
        /// Gets or sets a.
        /// </summary>
        /// <value>a.</value>
        public string? A { get; set; }

        /// <summary>
        /// Gets or sets the b.
        /// </summary>
        /// <value>The b.</value>
        public int B { get; set; }
    }
}