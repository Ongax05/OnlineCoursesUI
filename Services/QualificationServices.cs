using System.Net.Http;
using System.Net.Http.Json;
using Dtos;
using OnlineCoursesUI.Interfaces;

namespace OnlineCoursesUI.Services
{
    public class QualificationServices (HttpClient httpClient) : IQualificationServices
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<AverageDto> GetAverageQualificationByCourse(int CourseId)
        {
            var response = await _httpClient.GetFromJsonAsync<AverageDto>($"api/Qualification/ByCourse?CourseId={CourseId}");
            return response;
        }

        public async Task<List<QualificationDto>> GetQualificationsByUser(int UserId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<QualificationDto>>($"api/Qualification/ByUser?UserId={UserId}");
            return response;
        }

        public async Task<bool> PostQualification(QualificationDto qualificationDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Qualification", qualificationDto);
            return response.IsSuccessStatusCode;
        }
    }
}