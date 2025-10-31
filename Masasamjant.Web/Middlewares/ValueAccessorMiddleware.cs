namespace Masasamjant.Web.Middlewares
{
    /// <summary>
    /// Represents abstract <see cref="Middleware"/> that transfers value between HTTP context stores.
    /// </summary>
    public abstract class ValueAccessorMiddleware : Middleware
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ValueAccessorMiddleware"/> class.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/> of the next action.</param>
        /// <param name="getValueKey">The key of value to get.</param>
        /// <param name="setValueKey">The key of value to set.</param>
        protected ValueAccessorMiddleware(RequestDelegate next, string getValueKey, string setValueKey) 
            : base(next)
        {
            GetValueKey = getValueKey;
            SetValueKey = setValueKey;
        }

        /// <summary>
        /// Gets the key of value to get.
        /// </summary>
        public string GetValueKey { get; }

        /// <summary>
        /// Gets the key of value to set.
        /// </summary>
        public string SetValueKey { get; }

        /// <summary>
        /// Gets the <see cref="IHttpContextValueGetter"/>.
        /// </summary>
        protected abstract IHttpContextValueGetter ValueGetter { get; }

        /// <summary>
        /// Gets the <see cref="IHttpContextValueSetter"/>.
        /// </summary>
        protected abstract IHttpContextValueSetter ValueSetter { get; }

        /// <summary>
        /// Invoked when middleware is executed. Get value from specified <see cref="IHttpContextValueGetter"/> 
        /// and sets it to specified <see cref="IHttpContextValueSetter"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            var value = ValueGetter.GetHttpValue(context, GetValueKey);
            
            if (value != null)
                ValueSetter.SetHttpValue(context, SetValueKey, value);

            await Next(context);
        }
    }
}
