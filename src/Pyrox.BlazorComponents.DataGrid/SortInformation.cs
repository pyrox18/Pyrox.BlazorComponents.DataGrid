using Pyrox.BlazorComponents.DataGrid.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pyrox.BlazorComponents.DataGrid
{
    public class SortInformation<TItem> : IEquatable<SortInformation<TItem>>
    {
        public string Key { get; }
        public SortType Type { get; }

        public SortInformation(Expression<Func<TItem, object>> action, SortType type)
        {
            Key = action.GetMemberName();
            Type = type;
        }

        public SortInformation(string str)
        {
            var items = str.Split('-');
            Key = items[0];
            Type = (SortType)Enum.Parse(typeof(SortType), items[1]);
        }

        private SortInformation(string key, SortType type)
        {
            Key = key;
            Type = type;
        }

        public void Deconstruct(out string key, out SortType type) =>
            (key, type) = (Key, Type);

        public override string ToString()
        {
            return $"{Key}-{Type.ToString()}";
        }

        public static SortInformation<TItem> SortAscending(Expression<Func<TItem, object>> expression) =>
            new SortInformation<TItem>(expression, SortType.Ascending);

        public static SortInformation<TItem> SortDescending(Expression<Func<TItem, object>> expression) =>
            new SortInformation<TItem>(expression, SortType.Descending);

        public static IEnumerable<SortInformation<TItem>> GetSortInformationPairs()
        {
            var keyNames = typeof(TItem).GetProperties().Select(p => p.Name);

            var pairs = new List<SortInformation<TItem>>();

            foreach (var name in keyNames)
            {
                pairs.Add(new SortInformation<TItem>(name, SortType.Ascending));
                pairs.Add(new SortInformation<TItem>(name, SortType.Descending));
            }

            return pairs;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SortInformation<TItem>);
        }

        public bool Equals(SortInformation<TItem> other)
        {
            return Key == other.Key && Type == other.Type;
        }

        public override int GetHashCode()
        {
            var hashCode = 207421273;
            hashCode = hashCode * -1521134295 + Key.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(SortInformation<TItem> lhs, SortInformation<TItem> rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    return true;
                }

                return false;
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(SortInformation<TItem> lhs, SortInformation<TItem> rhs)
        {
            return !(lhs == rhs);
        }

    }
}
