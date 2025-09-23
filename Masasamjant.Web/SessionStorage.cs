using Masasamjant.Security;

namespace Masasamjant.Web
{
    /// <summary>
    /// Represents abstract <see cref="ISessionStorage"/>.
    /// </summary>
    public abstract class SessionStorage : ISessionStorage
    {
        /// <summary>
        /// Default value of <see cref="SessionIdentifierKey"/>.
        /// </summary>
        protected const string DefaultSessionIdentifierKey = "SESSION-IDENTIFIER-E4D397D25EC34DD3A819B0334388DF7A";
        
        /// <summary>
        /// Gets the key to store session identifier.
        /// </summary>
        protected virtual string SessionIdentifierKey
        {
            get { return DefaultSessionIdentifierKey; }
        }

        /// <summary>
        /// Clear session content.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Gets the session identifier.
        /// </summary>
        /// <returns>A unique string to identify session.</returns>
        public string GetSessionIdentifier()
        {
            var sessionIdentifier = GetString(SessionIdentifierKey);

            if (sessionIdentifier == null)
            {
                sessionIdentifier = CreateSessionIdentifier();
                SetString(SessionIdentifierKey, sessionIdentifier);
            }

            return sessionIdentifier;
        }

        /// <summary>
        /// Gets value from session.
        /// </summary>
        /// <param name="key">The session key.</param>
        /// <returns>A value in session or <c>null</c>.</returns>
        public abstract string? GetString(string key);

        /// <summary>
        /// Remove value from session.
        /// </summary>
        /// <param name="key">The session key.</param>
        public abstract void Remove(string key);

        /// <summary>
        /// Set value to session.
        /// </summary>
        /// <param name="key">The session key.</param>
        /// <param name="value">The value to set.</param>
        public abstract void SetString(string key, string value);

        /// <summary>
        /// Creates unique session identifier value.
        /// </summary>
        /// <returns>A unique session identifier.</returns>
        protected virtual string CreateSessionIdentifier()
        {
            var provider = new Base64SHA1Provider();
            return provider.CreateHash(Guid.NewGuid().ToString());
        }
    }
}
