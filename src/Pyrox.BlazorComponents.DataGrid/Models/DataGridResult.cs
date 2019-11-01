using System.Collections.Generic;

namespace Pyrox.BlazorComponents.DataGrid.Models
{
    public class DataGridResult<TItem>
    {
        public IEnumerable<TItem> Data { get; }
        public uint TotalItems { get; }

        public DataGridResult(IEnumerable<TItem> data, uint totalItems)
        {
            Data = data;
            TotalItems = totalItems;
        }
    }
}
