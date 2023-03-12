using Dictionary.WebApp.Insfrastructure.Services.Interfaces;

namespace Dictionary.WebApp.Insfrastructure.Services
{
    public class FavService : IFavService
    {
        private readonly HttpClient httpClient;

        public FavService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateEntryFav(Guid entryId)
        {
            await httpClient.PostAsync($"/api/favourite/Entry/{entryId}", null);
        }

        public async Task CreateEntryCommentFav(Guid entryCommentId)
        {
            await httpClient.PostAsync($"/api/favourite/EntryComment/{entryCommentId}", null);
        }

        public async Task DeleteEntryFav(Guid entryId)
        {
            await httpClient.PostAsync($"/api/favourite/DeleteEntryFav/{entryId}", null);
        }

        public async Task DeleteEntryCommentFav(Guid entryCommentId)
        {
            await httpClient.PostAsync($"/api/favourite/DeleteEntryCommentFav/{entryCommentId}", null);
        }
    }
}
