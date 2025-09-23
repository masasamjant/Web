namespace Masasamjant.Web.Actions
{
    /// <summary>
    /// Represents descriptor of action.
    /// </summary>
    public interface IActionDescriptor
    {
        /// <summary>
        /// Gets the area name or <c>null</c>, if not in area.
        /// </summary>
        string? AreaName { get; }

        /// <summary>
        /// Gets the controller name.
        /// </summary>
        string ControllerName { get; }

        /// <summary>
        /// Gets the action name.
        /// </summary>
        string ActionName { get; }
    }
}
