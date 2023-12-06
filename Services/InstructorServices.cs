using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Dtos;
using OnlineCoursesUI.Interfaces;
namespace OnlineCoursesUI.Services
{
    public class InstructorServices (HttpClient httpClient) : IInstructorServices
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<InstructorDto> GetInstructorByName(string Name)
        {
            var result = await _httpClient.GetFromJsonAsync<InstructorDto>($"/api/Instructor/ByName?Name={Name}");
            return result;
        }
    }
}