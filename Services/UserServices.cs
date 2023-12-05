using System.Net.Http.Json;
using Dtos;
using Microsoft.JSInterop;
using OnlineCoursesUI.Interfaces;

namespace OnlineCoursesUI.Services
{
    public class UserServices(HttpClient httpClient,IJSRuntime JSRuntime) : IUserServices
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IJSRuntime _JSRuntime = JSRuntime;

        public async Task<bool> RegisterUser(RegisterDto registerDto)
        {
            var result = await _httpClient.PostAsJsonAsync("api/User/register", registerDto);
            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<HttpResponseMessage> LoginUser(LoginDto loginDto)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/User/token", loginDto);
            return result;
        }
    }
}
