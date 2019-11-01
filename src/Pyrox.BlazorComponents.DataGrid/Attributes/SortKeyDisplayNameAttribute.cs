using System;

namespace Pyrox.BlazorComponents.DataGrid.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SortKeyDisplayNameAttribute : Attribute
    {
        public string DisplayName { get; }

        public SortKeyDisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
