using Microsoft.AspNetCore.Components;
using Pyrox.BlazorComponents.DataGrid.Components;
using Pyrox.BlazorComponents.DataGrid.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pyrox.BlazorComponents.DataGrid
{
    public class DataGridBase<TItem> : ComponentBase
    {
        [Inject] public IDataGridService<TItem> DataService { get; set; }

        protected List<TItem> items;
        protected int? totalItems;
        protected bool isLoading = false;
        protected bool isSaving = false;

        private int currentPage = 1;
        protected SortInformation<TItem> currentSort = null;
        private SortInformation<TItem> defaultSort;
        private string currentSearchQuery = string.Empty;
        protected int currentItemsPerPage = 10;

        protected PaginationControls paginationControls;

        [Parameter]
        public SortInformation<TItem> DefaultSort
        {
            get
            {
                return defaultSort;
            }
            set
            {
                defaultSort = value;
                currentSort = value;
            }
        }

        [Parameter]
        public RenderFragment GridHeader { get; set; }

        [Parameter]
        public RenderFragment<TItem> GridRow { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.WhenAll(new[]
            {
                GetItems(),
                GetItemCount()
            });
        }

        private async Task GetItems()
        {
            isLoading = true;

            items = await DataService.GetItemsAsync(
                currentPage,
                currentItemsPerPage,
                currentSort,
                currentSearchQuery);

            isLoading = false;
        }

        private async Task GetItemCount()
        {
            totalItems = null;
            totalItems = await DataService.GetItemCountAsync(currentSearchQuery);
        }

        protected async Task HandlePageChange(int page)
        {
            if (!isLoading)
            {
                currentPage = page;
                await GetItems();
            }
        }

        protected void Sort(ChangeEventArgs e)
        {
            currentSort = new SortInformation<TItem>((string)e.Value);
            paginationControls.GoToPage(1);
        }

        protected void Sort(SortInformation<TItem> sortInformation)
        {
            currentSort = sortInformation;
            paginationControls.GoToPage(1);
        }

        protected async Task Search(string query)
        {
            currentSearchQuery = query;
            await GetItemCount();
            paginationControls.GoToPage(1);
        }

        protected void ShowItemsPerPage(ChangeEventArgs e)
        {
            currentItemsPerPage = int.Parse((string)e.Value);
            paginationControls.ChangeItemsPerPage(currentItemsPerPage);
            paginationControls.GoToPage(1);
        }
    }
}
