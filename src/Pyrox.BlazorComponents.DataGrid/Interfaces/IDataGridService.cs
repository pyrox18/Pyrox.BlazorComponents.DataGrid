using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pyrox.BlazorComponents.DataGrid.Interfaces
{
    public interface IDataGridService<TItem, TKey>
    {
        Task<List<TItem>> GetItemsAsync(
            int pageNumber,
            int pageSize,
            SortInformation<TKey> sortInfo = null,
            string searchQuery = null);

        Task<int> GetItemCountAsync(string searchQuery = null);
    }
}
