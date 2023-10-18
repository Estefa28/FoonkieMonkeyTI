using FM.External.API.Interfaces;
using FM.External.API.Models;
using Newtonsoft.Json;

namespace FM.External.API.Services
{
    public class UserService : IUserService
    {
        const string UrlAPI = "https://reqres.in/api/";
        const string GetUsersEndpoint = "users?page=";

        public async Task<UserResponse> GetUsersAsync(int page)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(UrlAPI);

            var response = await client.GetAsync($"{GetUsersEndpoint}{page}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserResponse>(result);
            }

            throw new Exception("Failed Request");
        }
    }
}
