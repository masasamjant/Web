using Masasamjant.ComponentModel;
using Masasamjant.Security;
using Masasamjant.Security.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Web.ComponentModel
{
    /// <summary>
    /// Represents <see cref="IIdentifierManager"/> where scope of identifiers is session. The identifiers are stored using <see cref="IdentifierManager"/> class.
    /// </summary>
    /// <remarks>Instance of this class is thread-safe and can be used as singleton instance.</remarks>
    public sealed class SessionIdentifierManager : ITemporaryIdentifierManager
    {
        private readonly TemporaryIdentifierManager identifierManager;

        /// <summary>
        /// Initializes new default instance of the <see cref="SessionIdentifierManager"/> class that creates Base-64 SHA1 temporary identifiers.
        /// </summary>
        public SessionIdentifierManager()
            : this(new Base64SHA1Provider())
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="SessionIdentifierManager"/> class with specified <see cref="IStringHashProvider"/> to compute temporary identifiers.
        /// </summary>
        /// <param name="hashProvider">The <see cref="IStringHashProvider"/> to compute temporary identifiers.</param>
        public SessionIdentifierManager(IStringHashProvider hashProvider)
        {
            identifierManager = new TemporaryIdentifierManager(hashProvider);
        }

        /// <summary>
        /// Get temporary identifier for specified <typeparamref name="T"/> identifier.
        /// </summary>
        /// <typeparam name="T">The type of the identifier.</typeparam>
        /// <param name="key">The scope key.</param>
        /// <param name="identifier">The actual identifier.</param>
        /// <returns>A temporary identifier.</returns>
        public string GetTemporaryIdentifier<T>(string sessionIdentifier, T identifier) where T : notnull
        {
            return identifierManager.GetTemporaryIdentifier(sessionIdentifier, identifier);
        }

        /// <summary>
        /// Get temporary identifier for specified actual identifiers.
        /// </summary>
        /// <param name="key">The scope key.</param>
        /// <param name="identifiers">The actual identifier values.</param>
        /// <returns>A temporary identifier.</returns>
        public string GetTemporaryIdentifier(string sessionIdentifier, params object[] identifiers)
        {
            return identifierManager.GetTemporaryIdentifier(sessionIdentifier, identifiers);
        }

        /// <summary>
        /// Removes identifiers from specified key.
        /// </summary>
        /// <param name="key">The scope key.</param>
        public void RemoveIdentifiers(string sessionIdentifier)
        {
            identifierManager.RemoveIdentifiers(sessionIdentifier);
        }

        /// <summary>
        /// Tries to get the actual identifier for specified temporary identifier.
        /// </summary>
        /// <typeparam name="T">The type of the actual identifier.</typeparam>
        /// <param name="key">The scope key.</param>
        /// <param name="temporaryIdentifier">The temporary identifier.</param>
        /// <param name="identifier">The actual identifier when returns <c>true</c>; otherwise <c>null</c> or <c>default</c>.</param>
        /// <returns><c>true</c> if there was actual identifier for <paramref name="temporaryIdentifier"/>; <c>false</c> otherwise.</returns>
        public bool TryGetIdentifier<T>(string sessionIdentifier, string temporaryIdentifier, [MaybeNullWhen(false)] out T identifier)
        {
            return identifierManager.TryGetIdentifier(sessionIdentifier, temporaryIdentifier, out identifier);
        }

        /// <summary>
        /// Tries to get the actual identifiers for specified temporary identifier.
        /// </summary>
        /// <param name="key">The scope key.</param>
        /// <param name="temporaryIdentifier">The temporary identifier.</param>
        /// <param name="identifiers">The actual identifiers or empty array.</param>
        /// <returns><c>true</c> if there was actual identifier for <paramref name="temporaryIdentifier"/>; <c>false</c> otherwise.</returns>
        public bool TryGetIdentifier(string sessionIdentifier, string temporaryIdentifier, out object[] identifiers)
        {
            return identifierManager.TryGetIdentifier(sessionIdentifier, temporaryIdentifier, out identifiers);
        }
    }
}
