using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using OnlineCoursesUI.Interfaces;

namespace OnlineCoursesUI.Services
{
    public class QualificationServices(
        HttpClient httpClient,
        AuthenticationStateProvider AuthenticationProvider
    ) : IQualificationServices
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider AuthenticationProvider =
            (AuthenticationService)AuthenticationProvider;

        public async Task<AverageDto> GetAverageQualificationByCourse(int CourseId)
        {
            var UserData = await AuthenticationProvider.GetAuthenticationStateAsync();
            var token = UserData.User.FindFirst("UserToken").Value;

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/Qualification/ByCourse?CourseId={CourseId}"
            );

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AverageDto>();
            }

            throw new Exception("Error retrieving average qualification");
        }

        public async Task<List<QualificationDto>> GetQualificationsByUser(int UserId)
        {
            var UserData = await AuthenticationProvider.GetAuthenticationStateAsync();
            var token = UserData.User.FindFirst("UserToken").Value;

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/Qualification/ByUser?UserId={UserId}"
            );

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<QualificationDto>>();
            }

            throw new Exception("Error retrieving qualifications");
        }

        public async Task<bool> PostQualification(QualificationDto qualificationDto)
        {
            var UserData = await AuthenticationProvider.GetAuthenticationStateAsync();
            var token = UserData.User.FindFirst("UserToken").Value;

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Qualification")
            {
                Content = JsonContent.Create(qualificationDto)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
    }
}
