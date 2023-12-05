using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Dtos;
using OnlineCoursesUI.Interfaces;

namespace OnlineCoursesUI.Services
{
    public class CourseServices (HttpClient httpClient) : ICourseServices
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<IEnumerable<CourseDto>> GetCourses()
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<CourseDto>>("/api/Course");
            return result;
        }

        public async Task<bool> PostCourse (CourseDto courseDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Course", courseDto);
            return response.IsSuccessStatusCode;
        }
    }
}