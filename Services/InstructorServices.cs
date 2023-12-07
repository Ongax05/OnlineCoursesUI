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
    public class InstructorServices(
        HttpClient httpClient,
        AuthenticationStateProvider AuthenticationProvider
    ) : IInstructorServices
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider AuthenticationProvider =
            (AuthenticationService)AuthenticationProvider;

        public async Task<InstructorDto> GetInstructorByName(string Name)
        {
            var UserData = await AuthenticationProvider.GetAuthenticationStateAsync();
            var token = UserData.User.FindFirst("UserToken").Value;

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"/api/Instructor/ByName?Name={Name}"
            );

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<InstructorDto>();
            }

            throw new Exception("Error retrieving instructor");
        }
    }
}
