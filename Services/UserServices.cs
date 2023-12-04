using System.Net.Http.Json;
using Dtos;
using OnlineCoursesUI.Interfaces;

namespace OnlineCoursesUI.Services
{
    public class UserServices(HttpClient httpClient) : IUserServices
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<bool> RegisterUser(RegisterDto registerDto)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/User/register", registerDto);
            if (result.IsSuccessStatusCode)
                return true;
            else
                return false; 
        }

        public async Task<bool> SetValidationToken (LoginDto loginDto)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/User/token", loginDto);
            if (result.IsSuccessStatusCode)
                return true;
            else
                return false; 

        }
    }
}