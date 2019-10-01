using Pyrox.BlazorComponents.DataGrid.Exceptions;
using System;
using System.Collections.Generic;

namespace Pyrox.BlazorComponents.DataGrid
{
    public class SortInformation<TKey> : IEquatable<SortInformation<TKey>>
    {
        public TKey Key { get; }
        public SortType Type { get; }

        public SortInformation(TKey key, SortType type)
        {
            if (!(key is Enum))
            {
                throw new InvalidTypeParameterException(nameof(TKey), nameof(SortInformation<TKey>));
            }

            Key = key;
            Type = type;
        }

        public SortInformation(string str)
        {
            var items = str.Split('-');
            Key = (TKey)Enum.Parse(typeof(TKey), items[0]);
            Type = (SortType)Enum.Parse(typeof(SortType), items[1]);
        }

        public void Deconstruct(out TKey key, out SortType type) =>
            (key, type) = (Key, Type);

        public override string ToString()
        {
            return $"{Key.ToString()}-{Type.ToString()}";
        }

        public static SortInformation<TKey> SortAscending(TKey key) =>
            new SortInformation<TKey>(key, SortType.Ascending);

        public static SortInformation<TKey> SortDescending(TKey key) =>
            new SortInformation<TKey>(key, SortType.Descending);

        public static IEnumerable<SortInformation<TKey>> GetSortInformationPairs()
        {
            var keyNames = Enum.GetNames(typeof(TKey));

            var pairs = new List<SortInformation<TKey>>();

            foreach (var name in keyNames)
            {
                pairs.Add(new SortInformation<TKey>((TKey)Enum.Parse(typeof(TKey), name), SortType.Ascending));
                pairs.Add(new SortInformation<TKey>((TKey)Enum.Parse(typeof(TKey), name), SortType.Descending));
            }

            return pairs;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SortInformation<TKey>);
        }

        public bool Equals(SortInformation<TKey> other)
        {
            return (Key as Enum).CompareTo(other.Key as Enum) == 0 && Type == other.Type;
        }

        public override int GetHashCode()
        {
            var hashCode = 207421273;
            hashCode = hashCode * -1521134295 + EqualityComparer<TKey>.Default.GetHashCode(Key);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(SortInformation<TKey> lhs, SortInformation<TKey> rhs)
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

        public static bool operator !=(SortInformation<TKey> lhs, SortInformation<TKey> rhs)
        {
            return !(lhs == rhs);
        }

    }
}
