using Microsoft.AspNetCore.Mvc;

namespace Masasamjant.Web.Controllers
{
    /// <summary>
    /// Represents <see cref="ControllerBase"/> that implements <see cref="IApiController"/> interface.
    /// </summary>
    public abstract class ApiControllerBase : ControllerBase, IApiController
    {
    }
}
