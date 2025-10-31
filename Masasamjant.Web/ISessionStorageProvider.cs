namespace Masasamjant.Web
{
    /// <summary>
    /// Represents component that provides <see cref="ISessionStorage"/> implementation.
    /// </summary>
    public interface ISessionStorageProvider
    {
        /// <summary>
        /// Gets the <see cref="ISessionStorage"/> implementation.
        /// </summary>
        /// <returns>A <see cref="ISessionStorage"/>.</returns>
        /// <exception cref="InvalidOperationException">If session storage cannot be provided.</exception>
        ISessionStorage GetSessionStorage();
    }
}
