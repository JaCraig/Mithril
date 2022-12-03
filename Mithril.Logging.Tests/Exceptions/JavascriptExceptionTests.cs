using Mithril.Logging.Exceptions;
using Mithril.Tests.Helpers;

namespace Mithril.Logging.Tests.Exceptions
{
    public class JavascriptExceptionTests : TestBaseClass<JavascriptException>
    {
        public JavascriptExceptionTests()
        {
            TestObject = new JavascriptException();
            ObjectType = typeof(JavascriptException);
        }
    }
}