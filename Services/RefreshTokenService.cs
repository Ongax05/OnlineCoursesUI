using System.Net.Http.Headers;
using System.Net.Http.Json;
using Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Hosting;


namespace OnlineCoursesUI.Services
{
    public class RefreshTokenService(
        HttpClient httpClient,
        AuthenticationStateProvider AuthenticationProvider
    ) : BackgroundService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider AuthenticationProvider =
            (AuthenticationService)AuthenticationProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var UserData = await AuthenticationProvider.GetAuthenticationStateAsync();
                var token = UserData.User.FindFirst("RefreshToken").Value;

                var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    $"api/User/refresh-token?refreshToken={token}"
                );

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request, stoppingToken);

                var userData = await response.Content.ReadFromJsonAsync<DataUserDto>(cancellationToken: stoppingToken);
 
                var AuthService = (AuthenticationService)AuthenticationProvider;
                
                await AuthService.UpdateAuthenticationState(userData);
                Console.WriteLine("aaaa");
                await Task.Delay(TimeSpan.FromMinutes(9), stoppingToken);
            }
        }
    }
}
