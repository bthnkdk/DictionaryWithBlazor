using Dictionary.Common.Infrastructure.Exceptions;
using Dictionary.Common.Infrastructure.Result;
using Dictionary.Common.Models.CommandModels.User;
using Dictionary.Common.Models.QueriesModels;
using Dictionary.WebApp.Insfrastructure.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace Dictionary.WebApp.Insfrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<UserDetailViewModel> GetUserDetail(Guid? id)
        {
            var userDetail = await httpClient.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{id}");
            return userDetail;
        }

        public async Task<UserDetailViewModel> GetUserDetail(string userName)
        {
            var userDetail = await httpClient.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{userName}");
            return userDetail;
        }

        public async Task<bool> UpdateUser(UserDetailViewModel user)
        {
            var result = await httpClient.PostAsJsonAsync($"/api/user/update", user);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
        {
            var command = new ChangeUserPasswordCommand(newPassword, oldPassword, null);
            var httpResponse = await httpClient.PostAsJsonAsync($"/api/User/ChangePassword", command);

            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }
                return false;
            }

            return httpResponse.IsSuccessStatusCode;
        }
    }
}
