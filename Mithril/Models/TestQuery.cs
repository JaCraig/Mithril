using BigBook;
using Mithril.API.Query;
using Mithril.API.Query.BaseClasses;
using Mithril.API.Query.Interfaces;
using System.Security.Claims;

namespace Mithril.Models
{
    public class TestQuery : QueryBaseClass<TestVM>
    {
        public override IArgument[] Arguments { get; } = new IArgument[] {
            new Argument<string> { DefaultValue = "A", Description = "Description", Name = "name" },
            new Argument<int> { DefaultValue = 1, Description = "Description", Name = "count" }
        };

        public override Task<TestVM?> ResolveAsync(ClaimsPrincipal? arg, Arguments arguments)
        {
            var List = new List<TestVM2>();
            arguments.GetValue<int>("Count").Times(x => List.Add(new TestVM2() { A = arguments.GetValue<string>("Name") }));
            return Task.FromResult(new TestVM { A = List });
        }
    }

    public class TestVM
    {
        public List<TestVM2> A { get; set; }
    }

    public class TestVM2
    {
        public string? A { get; set; }
    }
}