using Blazored.LocalStorage;
using Dictionary.Common.Infrastructure.Exceptions;
using Dictionary.Common.Infrastructure.Result;
using Dictionary.Common.Models.QueriesModels;
using Dictionary.Common.Models.RequestModels;
using Dictionary.WebApp.Insfrastructure.Extensions;
using Dictionary.WebApp.Insfrastructure.Services.Interfaces;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Dictionary.WebApp.Insfrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient httpClient;
        private readonly ISyncLocalStorageService syncLocalStorageService;

        public IdentityService(HttpClient httpClient, ISyncLocalStorageService syncLocalStorageService)
        {
            this.httpClient = httpClient;
            this.syncLocalStorageService = syncLocalStorageService;
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public string GetUserToken()
        {
            return syncLocalStorageService.GetToken();
        }

        public string GetUserName()
        {
            return syncLocalStorageService.GetToken();
        }

        public Guid GetUserId()
        {
            return syncLocalStorageService.GetUserId();
        }

        public async Task<bool> Login(LoginUserCommand command)
        {
            string responseStr;
            var httpResponse = await httpClient.PostAsJsonAsync("/api/User/Login", command);

            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }

                return false;
            }

            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);

            if (!string.IsNullOrEmpty(response.Token)) //login success
            {
                syncLocalStorageService.SetToken(response.Token);
                syncLocalStorageService.SetUserName(response.UserName);
                syncLocalStorageService.SetUserId(response.Id);

                //TODO check after auth (authentication state provider)
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", response.UserName);

                return true;
            }

            return false;
        }

        public void LogOut()
        {
            syncLocalStorageService.RemoveItem(LocalStorageExtensions.TokenName);
            syncLocalStorageService.RemoveItem(LocalStorageExtensions.UserName);
            syncLocalStorageService.RemoveItem(LocalStorageExtensions.UserId);

            //TODO check after auth (authentication state provider)
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
