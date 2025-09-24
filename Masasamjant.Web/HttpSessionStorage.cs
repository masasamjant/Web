namespace Masasamjant.Web
{
    /// <summary>
    /// Represents <see cref="ISessionStorage"/> that use <see cref="ISession"/>.
    /// </summary>
    public class HttpSessionStorage : SessionStorage
    {
        private readonly ISession session;

        /// <summary>
        /// Initializes new instance of the <see cref="HttpSessionStorage"/> class.
        /// </summary>
        /// <param name="session">The <see cref="ISession"/>.</param>
        public HttpSessionStorage(ISession session)
        {
            this.session = session;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="HttpSessionStorage"/> class that use <see cref="HttpContext.Session"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        public HttpSessionStorage(HttpContext context)
            : this(context.Session)
        { }

        /// <summary>
        /// Clear session content.
        /// </summary>
        public override void Clear()
        {
            session.Clear();
        }

        /// <summary>
        /// Gets value from session.
        /// </summary>
        /// <param name="key">The session key.</param>
        /// <returns>A value in session or <c>null</c>.</returns>
        public override string? GetString(string key)
        {
            return session.GetString(key);
        }

        /// <summary>
        /// Remove value from session.
        /// </summary>
        /// <param name="key">The session key.</param>
        public override void Remove(string key)
        {
            session.Remove(key);
        }

        /// <summary>
        /// Set value to session.
        /// </summary>
        /// <param name="key">The session key.</param>
        /// <param name="value">The value to set.</param>
        public override void SetString(string key, string value)
        {
            session.SetString(key, value);
        }

        /// <summary>
        /// Creates unique session identifier.
        /// </summary>
        /// <returns>A unique session identifier.</returns>
        protected override string CreateSessionIdentifier()
        {
            return session.Id;
        }
    }
}
