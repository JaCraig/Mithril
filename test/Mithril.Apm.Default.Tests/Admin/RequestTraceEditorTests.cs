using Mithril.Apm.Default.Admin;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Admin
{
    /// <summary>
    /// Request trace editor tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RequestTraceEditor&gt;" />
    public class RequestTraceEditorTests : TestBaseClass<RequestTraceEditor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTraceEditorTests"/> class.
        /// </summary>
        public RequestTraceEditorTests()
        {
            TestObject = new RequestTraceEditor(null, null, null);
            ObjectType = typeof(RequestTraceEditor);
        }
    }
}