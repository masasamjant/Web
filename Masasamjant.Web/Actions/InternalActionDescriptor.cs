namespace Masasamjant.Web.Actions
{
    internal sealed class InternalActionDescriptor : IActionDescriptor
    {
        public InternalActionDescriptor(string actionName, string controllerName, string? areaName = null)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            AreaName = areaName;
        }

        public string? AreaName { get; }

        public string ControllerName { get; }

        public string ActionName { get; }
    }
}
