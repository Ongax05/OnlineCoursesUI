using System.Net.Http;
using System.Net.Http.Json;
using Dtos;
using OnlineCoursesUI.Interfaces;

namespace OnlineCoursesUI.Services
{
    public class QualificationServices (HttpClient httpClient) : IQualificationServices
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<bool> PostQualification(QualificationDto qualificationDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Qualification", qualificationDto);
            return response.IsSuccessStatusCode;
        }
    }
}