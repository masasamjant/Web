using Masasamjant.Reflection;
using Masasamjant.Resources;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Masasamjant.Web.Lists
{
    /// <summary>
    /// Provides helper methods to create <see cref="SelectListItem"/> instances.
    /// </summary>
    public static class SelectListHelper
    {
        /// <summary>
        /// Creates <see cref="SelectListItem"/>s from the <typeparamref name="TEnum"/> values.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
        /// <param name="current">The current value or <c>null</c>.</param>
        /// <param name="firstItem">The first item or <c>null</c>.</param>
        /// <param name="getDisabled">The delegate to get disabled state or <c>null</c>, if not checked aka all enabled.</param>
        /// <returns>A select list items.</returns>
        public static IEnumerable<SelectListItem> GetSelectListItems<TEnum>(TEnum? current = null, SelectListItem? firstItem = null, Func<TEnum, bool>? getDisabled = null) where TEnum : struct, Enum
        {
            var values = Enum.GetValues<TEnum>();

            if (firstItem != null)
                yield return firstItem;

            foreach (var value in values)
            {
                var itemValue = value.ToString();
                var itemText = value.GetResourceStringOrName();
                var disabled = getDisabled != null ? getDisabled(value) : false;
                yield return new SelectListItem(itemValue, itemText, current.HasValue && value.Equals(current.Value), disabled);
            }
        }

        /// <summary>
        /// Creates <see cref="SelectListItem"/>s from the <typeparamref name="T"/> values.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="values">The values.</param>
        /// <param name="getText">The delegate to get value of <see cref="SelectListItem.Text"/>.</param>
        /// <param name="getValue">The delegate to get value of <see cref="SelectListItem.Value"/>.</param>
        /// <param name="current">The current value or <c>null</c>.</param>
        /// <param name="firstItem">The first item or <c>null</c>.</param>
        /// <param name="getDisabled">The delegate to get disabled state or <c>null</c>, if not checked aka all enabled.</param>
        /// <returns>A select list items.</returns>
        public static IEnumerable<SelectListItem> GetSelectListItems<T>(IEnumerable<T> values, Func<T, string> getText, Func<T, string> getValue, T? current = default, SelectListItem? firstItem = null, Func<T, bool>? getDisabled = null)
        {
            if (firstItem != null)
                yield return firstItem;

            foreach (var value in values)
            {
                var itemValue = getValue(value);
                var itemText = getText(value);
                var disabled = getDisabled != null ? getDisabled(value) : false;
                yield return new SelectListItem(itemValue, itemText, current is not null && Equals(current, value), disabled);
            }
        }

        /// <summary>
        /// Gets or creates <see cref="SelectListGroup"/> with specified name. If <paramref name="createNewGroup"/> is <c>true</c>, then 
        /// always creates new group. Otherwise search group with specified name from <paramref name="items"/> and if found, then returns 
        /// exiting group and otherwise creates new group.
        /// </summary>
        /// <param name="items">The select list items.</param>
        /// <param name="groupName">The group name.</param>
        /// <param name="createNewGroup"><c>true</c> to always create new group; <c>false</c> otherwise.</param>
        /// <returns></returns>
        public static SelectListGroup GetOrCreateGroup(IEnumerable<SelectListItem> items, string groupName, bool createNewGroup = false)
        {
            if (createNewGroup || !items.Any())
                return new SelectListGroup() { Name = groupName };

            var group = items.Where(x => x.Group != null && x.Group.Name == groupName).Select(x => x.Group).FirstOrDefault();

            if (group == null)
                group = new SelectListGroup() { Name = groupName };

            return group;
        }

        /// <summary>
        /// Gets different groups from specified select list items.
        /// </summary>
        /// <param name="items">The select list items.</param>
        /// <returns>A different select list groups.</returns>
        public static IEnumerable<SelectListGroup> GetSelectListGroups(this IEnumerable<SelectListItem> items)
        {
            var groups = new HashSet<SelectListGroup>();

            foreach (var item in items)
            {
                if (item.Group == null || groups.Contains(item.Group))
                    continue;
                else
                    groups.Add(item.Group);
            }

            return groups;
        }

        /// <summary>
        /// Sets <see cref="SelectListItem.Selected"/> based on result of invoking specified delegate.
        /// </summary>
        /// <param name="items">The select list items.</param>
        /// <param name="isSelected">The delegate to check is item selected or not.</param>
        /// <returns>A select list items that was set as selected.</returns>
        public static IEnumerable<SelectListItem> Select(this IEnumerable<SelectListItem> items, Func<SelectListItem, bool> isSelected)
        {
            foreach (var item in items)
            {
                if (isSelected(item))
                {
                    item.Selected = true;
                    yield return item;
                }
                else
                {
                    item.Selected = false;
                }
            }
        }

        /// <summary>
        /// Gets select list items withing group specified by name.
        /// </summary>
        /// <param name="items">The select list items.</param>
        /// <param name="groupName">The group name.</param>
        /// <returns>A select list items belonging to groups with name specified by <paramref name="groupName"/>.</returns>
        public static IEnumerable<SelectListItem> GetSelectGroupItems(this IEnumerable<SelectListItem> items, string groupName)
        {
            return items.Where(item => item.Group != null && item.Group.Name == groupName);
        }

        /// <summary>
        /// Gets select list items belonging to specified select list group.
        /// </summary>
        /// <param name="items">The select list item.</param>
        /// <param name="group">The select list group.</param>
        /// <returns>A select list items belonging to <paramref name="group"/>.</returns>
        public static IEnumerable<SelectListItem> GetSelectGroupItems(this IEnumerable<SelectListItem> items, SelectListGroup group)
        {
            return items.Where(item => item.Group != null && ReferenceEquals(item.Group, group));
        }

        public static void Enable(this IEnumerable<SelectListItem> items, Func<SelectListItem, bool> isEnabled)
        {
            foreach (var item in items)
            {
                if (isEnabled(item))
                    item.Disabled = false;
                else
                    item.Disabled = true;
            }
        }

        public static void Enable(this IEnumerable<SelectListItem> items, SelectListGroup? group)
        {
            if (group != null)
                Enable(items, x => x.Group != null && ReferenceEquals(x.Group, group));
            else
                Enable(items, x => x.Group == null);
        }

        public static void Disable(this IEnumerable<SelectListItem> items, Func<SelectListItem, bool> isDisabled)
        {
            foreach (var item in items)
            {
                if (isDisabled(item))
                    item.Disabled = true;
                else
                    item.Disabled = false;
            }
        }

        public static void Disable(this IEnumerable<SelectListItem> items, SelectListGroup? group)
        {
            if (group != null)
                Disable(items, x => x.Group != null && ReferenceEquals(x.Group, group));
            else
                Disable(items, x => x.Group == null);
        }

        public static IEnumerable<SelectListItem> Regroup(this IEnumerable<SelectListItem> items, SelectListGroup oldGroup, SelectListGroup newGroup)
        {
            if (ReferenceEquals(oldGroup, newGroup))
                yield break;

            foreach (var item in GetSelectGroupItems(items, oldGroup))
            {
                item.Group = newGroup;
                yield return item;
            }
        }

        /// <summary>
        /// Group select list items without group to specified <see cref="SelectListGroup"/>.
        /// </summary>
        /// <param name="items">The select list items.</param>
        /// <param name="group">The group to set.</param>
        /// <returns>A select list items that was grouped to <paramref name="group"/>.</returns>
        public static IEnumerable<SelectListItem> Group(this IEnumerable<SelectListItem> items, SelectListGroup group)
        {
            foreach (var item in items)
            {
                if (item.Group == null)
                {
                    item.Group = group;
                    yield return item;
                }
            }
        }
    }
}
