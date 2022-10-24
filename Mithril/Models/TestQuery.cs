using Mithril.API.Query;
using Mithril.API.Query.Interfaces;
using System.Security.Claims;

namespace Mithril.Models
{
    public class TestQuery : IQuery<TestVM>
    {
        public Argument[] Arguments { get; } = Array.Empty<Argument>();
        public string DeprecationReason { get; }
        public string Description { get; } = "Test query";
        public string Name { get; } = "TestQueryName";
        public bool? Nullable { get; } = true;
        public Resolver<TestVM> Resolver { get; } = new Resolver<TestVM>(Resolve);

        public Type ReturnType { get; } = typeof(TestVM);

        private static Task<TestVM?> Resolve(ClaimsPrincipal? arg, Arguments arguments)
        {
            return Task.FromResult(new TestVM { A = "Testing" });
        }
    }

    public class TestVM
    {
        public string? A { get; set; }
    }
}