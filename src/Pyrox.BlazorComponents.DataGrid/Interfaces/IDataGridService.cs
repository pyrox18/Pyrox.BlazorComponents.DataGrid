using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pyrox.BlazorComponents.DataGrid.Interfaces
{
    public interface IDataGridService<TItem>
    {
        Task<List<TItem>> GetItemsAsync(
            int pageNumber,
            int pageSize,
            SortInformation<TItem> sortInfo = null,
            string searchQuery = null,
            object parameters = null);

        Task<int> GetItemCountAsync(
            string searchQuery = null,
            object parameters = null);
    }
}
