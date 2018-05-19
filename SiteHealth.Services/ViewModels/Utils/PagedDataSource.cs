using SiteHealth.Services.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Services.ViewModels.Utils
{
    public class PagedDataSource<T> where T: ViewModelBase
    {
        public T[] Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public bool IsEmpty => TotalItems == 0;
        public int TotalPages => (TotalItems / PageSize) + (TotalItems % PageSize == 0 ? 0 : 1);
        public int ItemsFrom => (CurrentPage - 1) * PageSize + 1;
        public int ItemsTo => ((CurrentPage) * PageSize > TotalItems) ? TotalItems : (CurrentPage) * PageSize;

        public static async Task<PagedDataSource<T>> CreateFromIQueryableAsync(IQueryable<T> query, int page, int pageSize)
        {
            var total = await query.CountAsync();
            var items = await query.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
            return new PagedDataSource<T>
            {
                Items = items,
                TotalItems = total,
                CurrentPage = page,
                PageSize = pageSize
            };
        }
    }

    public static class IQueryablePagedDataSourceExtensions
    {
        public static async Task<PagedDataSource<T>> ConvertToPagedDataSourceAsync<T>(this IQueryable<T> query, int page, int pageSize = 5) where T: ViewModelBase
        {
            return await PagedDataSource<T>.CreateFromIQueryableAsync(query, page, pageSize);
        }
    }
}
