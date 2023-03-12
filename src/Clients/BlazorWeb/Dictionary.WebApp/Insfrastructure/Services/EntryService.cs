using Dictionary.Common.Models.CommandModels;
using Dictionary.Common.Models.Pages;
using Dictionary.Common.Models.QueriesModels;
using Dictionary.WebApp.Insfrastructure.Services.Interfaces;
using System.Net.Http.Json;

namespace Dictionary.WebApp.Insfrastructure.Services
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient httpClient;

        public EntryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<GetEntriesViewModel>> GetEntries()
        {
            var result = await httpClient.GetFromJsonAsync<List<GetEntriesViewModel>>("/api/entry?todaysEntries=false&count=30");
            return result;
        }

        public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
        {
            var result = await httpClient.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/entry{entryId}");
            return result;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPagesEntries(int page, int pageSize)
        {
            var result = await httpClient.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/mainpageentries?page={page}&pageSize={pageSize}");
            return result;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null)
        {
            var result = await httpClient.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/UserEntries?userName={userName}&page={page}&apageSize={pageSize}");
            return result;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
        {
            var result = await httpClient.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"/api/entry/{entryId}?page={page}&pageSize={pageSize}");
            return result;
        }

        public async Task<Guid> CreateEntry(CreateEntryCommand command)
        {
            var result = await httpClient.PostAsJsonAsync("/api/entry/CreateEntry", command);

            if (!result.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await result.Content.ReadAsStringAsync();
            return new Guid(guidStr.Trim('"'));
        }

        public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
        {
            var result = await httpClient.PostAsJsonAsync("/api/entry/CreateEntryComment", command);

            if (!result.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await result.Content.ReadAsStringAsync();
            return new Guid(guidStr.Trim('"'));
        }

        public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
        {
            var result = await httpClient.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/entry/Search?searchText={searchText}");
            return result;
        }
    }
}
