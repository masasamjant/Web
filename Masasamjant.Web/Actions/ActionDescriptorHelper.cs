using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Masasamjant.Web.Actions
{
    /// <summary>
    /// Provides helper methods to <see cref="ActionDescriptor"/> class and <see cref="IActionDescriptor"/> interface.
    /// </summary>
    public static class ActionDescriptorHelper
    {
        /// <summary>
        /// Gets the action name of specified <see cref="ActionDescriptor"/>, if it is <see cref="ControllerActionDescriptor"/>.
        /// </summary>
        /// <param name="actionDescriptor">The <see cref="ActionDescriptor"/>.</param>
        /// <returns>A action name or empty string.</returns>
        public static string GetActionName(this ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                return controllerActionDescriptor.ActionName;

            return string.Empty;
        }

        /// <summary>
        /// Gets the controller name of specified <see cref="ActionDescriptor"/>, if it is <see cref="ControllerActionDescriptor"/>.
        /// </summary>
        /// <param name="actionDescriptor">The <see cref="ActionDescriptor"/>.</param>
        /// <returns>A controller name or empty string.</returns>
        public static string GetControllerName(this ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                return controllerActionDescriptor.ControllerName;

            return string.Empty;
        }

        /// <summary>
        /// Gets the area name of specified <see cref="ActionDescriptor"/>, if it is <see cref="PageActionDescriptor"/> 
        /// or <see cref="ControllerActionDescriptor"/> and route values has area.
        /// </summary>
        /// <param name="actionDescriptor">The <see cref="ActionDescriptor"/>.</param>
        /// <returns>A area name or <c>null</c>.</returns>
        public static string? GetAreaName(this ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                if (controllerActionDescriptor.RouteValues.ContainsKey("area"))
                    return controllerActionDescriptor.RouteValues["area"];
            }
            else if (actionDescriptor is PageActionDescriptor pageActionDescriptor)
                return pageActionDescriptor.AreaName;

            return null;
        }

        /// <summary>
        /// Creates <see cref="IActionDescriptor"/> from specified <see cref="ActionDescriptor"/>.
        /// </summary>
        /// <param name="actionDescriptor">The <see cref="ActionDescriptor"/>.</param>
        /// <returns>A <see cref="IActionDescriptor"/>.</returns>
        public static IActionDescriptor AsInterface(this ActionDescriptor actionDescriptor)
            => new InternalActionDescriptor(actionDescriptor.GetActionName(), actionDescriptor.GetControllerName(), actionDescriptor.GetAreaName());

        /// <summary>
        /// Creates <see cref="IActionDescriptor"/> from specified <see cref="ActionDescriptor"/> or values set explicitly.
        /// </summary>
        /// <param name="actionDescriptor">The <see cref="ActionDescriptor"/>.</param>
        /// <param name="action">The action name or <c>null</c> to get action name from <paramref name="actionDescriptor"/>.</param>
        /// <param name="controller">The controller name or <c>null</c> to get controller name from <paramref name="actionDescriptor"/>.</param>
        /// <param name="area">The area name or <c>null</c> to get area name from <paramref name="actionDescriptor"/>.</param>
        /// <returns>A <see cref="IActionDescriptor"/>.</returns>
        public static IActionDescriptor AsInterface(this ActionDescriptor actionDescriptor, string? action = null, string? controller = null, string? area = null)
        {
            if (area == null)
                area = actionDescriptor.GetAreaName();

            if (controller == null)
                controller = actionDescriptor.GetControllerName();

            if (action == null)
                action = actionDescriptor.GetActionName();

            return new InternalActionDescriptor(action, controller, area);
        }

        /// <summary>
        /// Creates <see cref="IActionDescriptor"/> from specified values.
        /// </summary>
        /// <param name="actionName">The action name.</param>
        /// <param name="controllerName">The controller name.</param>
        /// <param name="areaName">The area name or <c>null</c>.</param>
        /// <returns>A <see cref="IActionDescriptor"/>.</returns>
        public static IActionDescriptor CreateActionDescriptor(string actionName, string controllerName, string? areaName = null)
            => new InternalActionDescriptor(actionName, controllerName, areaName);

        /// <summary>
        /// Generates URL with path for an action specified by <see cref="IActionDescriptor"/>.
        /// </summary>
        /// <param name="url">The <see cref="IUrlHelper"/>.</param>
        /// <param name="actionDescriptor">The <see cref="IActionDescriptor"/>.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>A URL with path for action described by <paramref name="actionDescriptor"/>.</returns>
        public static string Action(this IUrlHelper url, IActionDescriptor actionDescriptor, object? routeValues = null)
        {
            var routeValueDictionary = new RouteValueDictionary(routeValues);

            if (!string.IsNullOrWhiteSpace(actionDescriptor.AreaName))
                routeValueDictionary["area"] = actionDescriptor.AreaName;

            return url.Action(actionDescriptor.ActionName, actionDescriptor.ControllerName, routeValueDictionary) ?? string.Empty;
        }

        /// <summary>
        /// Generates URL with path for an action specified by <see cref="ActionDescriptor"/>.
        /// </summary>
        /// <param name="url">The <see cref="IUrlHelper"/>.</param>
        /// <param name="actionDescriptor">The <see cref="ActionDescriptor"/>.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>A URL with path for action described by <paramref name="actionDescriptor"/>.</returns>
        public static string Action(this IUrlHelper url, ActionDescriptor actionDescriptor, object? routeValues = null)
            => Action(url, actionDescriptor.AsInterface(), routeValues);
    }
}
