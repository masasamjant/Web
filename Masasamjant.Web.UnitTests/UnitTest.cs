using Microsoft.AspNetCore.Http;

namespace Masasamjant.Web
{
    public abstract class UnitTest
    {
        protected HttpContext CreateHttpContext()
        {
            return new DefaultHttpContext();
        }
    }
}
