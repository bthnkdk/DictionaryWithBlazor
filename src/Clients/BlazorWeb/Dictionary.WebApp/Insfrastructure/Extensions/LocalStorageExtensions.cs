using Blazored.LocalStorage;

namespace Dictionary.WebApp.Insfrastructure.Extensions
{
    public static class LocalStorageExtensions
    {
        public const string TokenName = "token";
        public const string UserName = "username";
        public const string UserId = "userid";

        public static bool IsUserLoggedIn(this ISyncLocalStorageService localStorageService)
        {
            return !string.IsNullOrEmpty(GetToken(localStorageService));
        }

        public static string GetUserName(this ISyncLocalStorageService localStorageService)
        {
            return localStorageService.GetItem<string>(UserName);
        }

        public static void SetUserName(this ISyncLocalStorageService localStorageService, string value)
        {
            localStorageService.SetItem(UserName, value);
        }

        public static Guid GetUserId(this ISyncLocalStorageService localStorageService)
        {
            return localStorageService.GetItem<Guid>(UserId);
        }

        public static void SetUserId(this ISyncLocalStorageService localStorageService, Guid id)
        {
            localStorageService.SetItem(UserId, id);
        }

        public static string GetToken(this ISyncLocalStorageService localStorageService)
        {
            var token = localStorageService.GetItem<string>(TokenName);

            if (string.IsNullOrWhiteSpace(token))
                token = string.Empty;

            return token;
        }

        public static void SetToken(this ISyncLocalStorageService localStorageService, string value)
        {
            localStorageService.SetItem(TokenName, value);
        }
    }
}
