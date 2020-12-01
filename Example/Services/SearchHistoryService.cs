using Apsy.Elemental.Core.Identity;
using Apsy.Elemental.Example.Web.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Apsy.Elemental.Example.Web.Services
{
    public class SearchHistoryService
    {
        private readonly SingltonDataContextService singltonDataContextService;

        public SearchHistoryService(SingltonDataContextService singltonDataContextService)
        {
            this.singltonDataContextService = singltonDataContextService;
        }

        public async Task<SearchHistory> AddSearchHistory(SearchHistory searchHistory)
        {
            try
            {
                return await singltonDataContextService.Execute<SearchHistory>(async dataContext =>
                {
                    searchHistory.CreatedOn = DateTime.Now;
                    var searchHistoryEntry = dataContext.SearchHistory.Add(searchHistory);
                    await dataContext.SaveChangesAsync();
                    return searchHistoryEntry.Entity;
                });
            }
            catch (AuthException ex)
            {
                throw new Exception("Error while creating a searchHistory");
            }

        }

    }
}
