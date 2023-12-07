using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using OnlineCoursesUI.Interfaces;

namespace OnlineCoursesUI.Services
{
    public class CourseServices(
        HttpClient httpClient,
        AuthenticationStateProvider AuthenticationProvider
    ) : ICourseServices
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider AuthenticationProvider =
            (AuthenticationService)AuthenticationProvider;

        public async Task<IEnumerable<CourseDto>> GetCourses()
        {
            var UserData = await AuthenticationProvider.GetAuthenticationStateAsync();
            var token = UserData.User.FindFirst("UserToken").Value;

            var request = new HttpRequestMessage(HttpMethod.Get, "api/Course");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<CourseDto>>();
            }

            throw new Exception("Error retrieving courses");
        }

        public async Task<bool> PostCourse(CourseDto courseDto)
        {
            var UserData = await AuthenticationProvider.GetAuthenticationStateAsync();
            var token = UserData.User.FindFirst("UserToken").Value;

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Course")
            {
                Content = JsonContent.Create(courseDto)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        public async Task<CourseWithEntities> GetCourseById(int Id)
        {
            var UserData = await AuthenticationProvider.GetAuthenticationStateAsync();
            var token = UserData.User.FindFirst("UserToken").Value;

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Course/ById?Id={Id}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CourseWithEntities>();
            }

            throw new Exception("Error retrieving course");
        }
    }
}
