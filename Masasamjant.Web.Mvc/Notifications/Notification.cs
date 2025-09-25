using Masasamjant.Serialization;
using System.Text.Json.Serialization;

namespace Masasamjant.Web.Notifications
{
    /// <summary>
    /// Represents notification displayed in view.
    /// </summary>
    public class Notification : ViewModel, IJsonSerializable
    {
        /// <summary>
        /// Minimum value of <see cref="HideTimeoutSeconds"/>.
        /// </summary>
        public const int MinHideTimeoutSeconds = 0;

        /// <summary>
        /// Maximum value of <see cref="HideTimeoutSeconds"/>.
        /// </summary>
        public const int MaxHideTimeoutSeconds = 300;

        /// <summary>
        /// Initializes new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="notificationType">The notification type.</param>
        /// <param name="title">The notification title.</param>
        /// <param name="message">The notification message.</param>
        /// <param name="hideTimeoutSeconds">The hide timeout in seconds.</param>
        /// <remarks><see cref="ShowCloseButton"/> will be <c>false</c> if <paramref name="hideTimeoutSeconds"/> is greater than 0.</remarks>
        /// <exception cref="ArgumentException">If value of <paramref name="notificationType"/> is not defined.</exception> 
        public Notification(NotificationType notificationType, string? title, string? message, int hideTimeoutSeconds)
            : this(notificationType, title, message, false, hideTimeoutSeconds)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="notificationType">The notification type.</param>
        /// <param name="title">The notification title.</param>
        /// <param name="message">The notification message.</param>
        /// <param name="showCloseButton"><c>true</c> if close button is shown; <c>false</c> otherwise.</param>
        /// <remarks><see cref="HideTimeoutSeconds"/> will be <see cref="MaxHideTimeoutSeconds"/>.</remarks>
        /// <exception cref="ArgumentException">If value of <paramref name="notificationType"/> is not defined.</exception> 
        public Notification(NotificationType notificationType, string? title, string? message, bool showCloseButton)
            : this(notificationType, title, message, showCloseButton, MaxHideTimeoutSeconds)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="notificationType">The notification type.</param>
        /// <param name="title">The notification title.</param>
        /// <param name="message">The notification message.</param>
        /// <param name="showCloseButton"><c>true</c> if close button is shown; <c>false</c> otherwise.</param>
        /// <param name="hideTimeoutSeconds">The hide timeout in seconds.</param>
        /// <remarks><see cref="ShowCloseButton"/> will be <c>true</c> also if <paramref name="showCloseButton"/> false and <paramref name="hideTimeoutSeconds"/> is less than or equal to 0.</remarks>
        /// <exception cref="ArgumentException">If value of <paramref name="notificationType"/> is not defined.</exception>
        public Notification(NotificationType notificationType, string? title, string? message, bool showCloseButton, int hideTimeoutSeconds)
        {
            NotificationType = Enum.IsDefined(notificationType) ? notificationType : throw new ArgumentException("The value is not defined.", nameof(notificationType));
            Title = string.IsNullOrWhiteSpace(title) ? string.Empty : title.Trim();
            Message = string.IsNullOrWhiteSpace(message) ? string.Empty : message.Trim();
            HideTimeoutSeconds = Math.Min(Math.Max(MinHideTimeoutSeconds, hideTimeoutSeconds), MaxHideTimeoutSeconds);
            ShowCloseButton = showCloseButton || HideTimeoutSeconds == MinHideTimeoutSeconds;
        }

        /// <summary>
        /// Initializes new default instance of the <see cref="Notification"/> class.
        /// </summary>
        public Notification()
        { }

        /// <summary>
        /// Gets the notification title.
        /// </summary>
        [JsonInclude]
        public string Title { get; internal set; } = string.Empty;

        /// <summary>
        /// Gets the notification message.
        /// </summary>
        [JsonInclude]
        public string Message { get; internal set; } = string.Empty;

        /// <summary>
        /// Gets the notification type.
        /// </summary>
        [JsonInclude]
        public NotificationType NotificationType { get; internal set; }

        /// <summary>
        /// Gets if or not close button should be visible. <c>true</c> if explicitly set or if <see cref="HideTimeoutSeconds"/> is 
        /// equal to <see cref="MinHideTimeoutSeconds"/> and <c>false</c> otherwise.
        /// </summary>
        [JsonInclude]
        public bool ShowCloseButton { get; internal set; }

        /// <summary>
        /// Gets the time, in seconds, after the notification is hide. If equal to <see cref="MinHideTimeoutSeconds"/>, then not hide until 
        /// user closes or <see cref="MaxHideTimeoutSeconds"/> is reached.
        /// </summary>
        [JsonInclude]
        public int HideTimeoutSeconds { get; internal set; }

        /// <summary>
        /// Gets if or not notification should be visible. <c>true</c> if <see cref="Title"/> or <see cref="Message"/>, has
        /// non-empty string and <c>false</c> otherwise.
        /// </summary>
        [JsonIgnore]
        public bool IsVisible
        {
            get { return !string.IsNullOrWhiteSpace(Title) || !string.IsNullOrWhiteSpace(Message); }
        }
    }
}
