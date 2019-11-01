using Pyrox.BlazorComponents.DataGrid.Models;
using System.Threading.Tasks;

namespace Pyrox.BlazorComponents.DataGrid.Interfaces
{
    public interface IDataGridService<TItem>
    {
        Task<DataGridResult<TItem>> GetItemsAsync(
            int pageNumber,
            int pageSize,
            SortInformation<TItem> sortInfo = null,
            string searchQuery = null,
            object parameters = null);
    }
}
