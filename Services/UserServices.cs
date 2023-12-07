using System.Net.Http.Headers;
using System.Net.Http.Json;
using Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using OnlineCoursesUI.Interfaces;

namespace OnlineCoursesUI.Services
{
    public class UserServices(
        HttpClient httpClient,
        AuthenticationStateProvider AuthenticationProvider
    ) : IUserServices
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider AuthenticationProvider =
            (AuthenticationService)AuthenticationProvider;

        public async Task<bool> RegisterUser(RegisterDto registerDto)
        {
            var UserData = await AuthenticationProvider.GetAuthenticationStateAsync();
            var token = UserData.User.FindFirst("UserToken").Value;

            var request = new HttpRequestMessage(HttpMethod.Post, "api/User/register")
            {
                Content = JsonContent.Create(registerDto)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        public async Task<HttpResponseMessage> LoginUser(LoginDto loginDto)
        {
            var result = await _httpClient.PostAsJsonAsync("api/User/token", loginDto);
            return result;
        }
    }
}
