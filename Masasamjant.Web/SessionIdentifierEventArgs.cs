namespace Masasamjant.Web
{
    /// <summary>
    /// Represents arguments for events associated with session identifier.
    /// </summary>
    public class SessionIdentifierEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes new instance of the <see cref="SessionIdentifierEventArgs"/> class.
        /// </summary>
        /// <param name="sessionIdentifier">The unique session identifier.</param>
        public SessionIdentifierEventArgs(string sessionIdentifier)
        {
            SessionIdentifier = sessionIdentifier;
        }

        /// <summary>
        /// Gets the unique session identifier.
        /// </summary>
        public string SessionIdentifier { get; }
    }
}
