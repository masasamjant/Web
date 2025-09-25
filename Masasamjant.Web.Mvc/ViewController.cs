using Microsoft.AspNetCore.Mvc;

namespace Masasamjant.Web
{
    public abstract class ViewController : Controller
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ViewController"/> class.
        /// </summary>
        protected ViewController() 
        { }

        /// <summary>
        /// Gets the <see cref="ISessionStorage"/>.
        /// </summary>
        protected virtual ISessionStorage SessionStorage
        {
            get { return new HttpSessionStorage(HttpContext); }
        }
    }
}
