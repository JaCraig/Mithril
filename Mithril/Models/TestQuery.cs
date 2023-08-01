using BigBook;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.Query;
using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.Core.Abstractions.Modules.Features;
using Mithril.Core.Abstractions.Modules.Interfaces;
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
        /// Initializes a new instance of the <see cref="TestQuery"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="featureManager"></param>
        public TestQuery(ILogger<TestQuery>? logger, IFeatureManager? featureManager)
            : base(logger, featureManager)
        {
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public override IArgument[] Arguments { get; } = new IArgument[] {
            new Argument<string> { DefaultValue = "A", Description = "Description", Name = "name" },
            new Argument<int> { DefaultValue = 1, Description = "Description", Name = "count" }
        };

        /// <summary>
        /// Gets the features associated with this command.
        /// </summary>
        /// <value>The features associated with this command.</value>
        public override IFeature[] Features => new IFeature[] { new GenericFeature("ExampleFeature", "My Category", "Some description") };

        /// <summary>
        /// Resolves the asynchronous.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public override Task<TestVM?> ResolveAsync(ClaimsPrincipal? arg, Arguments arguments)
        {
            if (!IsFeatureEnabled())
                return Task.FromResult<TestVM?>(null);
            var List = new List<TestVM2>();
            arguments.GetValue<int>("Count").Times(_ => List.Add(new TestVM2() { A = arguments.GetValue<string>("Name") }));
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

        /// <summary>
        /// Examples the method.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public TestVM2 ExampleMethod(string a = "A", int b = 2)
        {
            return new TestVM2 { A = a, B = b };
        }

        /// <summary>
        /// Examples the method2.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        [ApiDepricationReason("Because it is old or something")]
        public string ExampleMethod2(string a, int b) => a + b;
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