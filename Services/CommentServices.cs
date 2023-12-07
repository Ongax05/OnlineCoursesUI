using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.SessionStorage;
using Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using OnlineCoursesUI.Interfaces;

namespace OnlineCoursesUI.Services
{
    public class CommentServices(
        HttpClient httpClient,
        AuthenticationStateProvider AuthenticationProvider
    ) : ICommentServices
    {
        private readonly AuthenticationStateProvider AuthenticationProvider =
            (AuthenticationService)AuthenticationProvider;
        private readonly HttpClient _httpClient = httpClient;

        public async Task<bool> PostComment(CommentDto commentDto)
        {
            var UserData = await AuthenticationProvider.GetAuthenticationStateAsync();
            var token = UserData.User.FindFirst("UserToken").Value;

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Comment")
            {    
                Content = JsonContent.Create(commentDto)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CommentDto>> GetCommentsByCourse(int CourseId)
        {
            var UserData = await AuthenticationProvider.GetAuthenticationStateAsync();
            var token = UserData.User.FindFirst("UserToken").Value;

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/Comment/ByCourse?CourseId={CourseId}"
            );

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            return await response.Content.ReadFromJsonAsync<List<CommentDto>>();
        }

        public async Task<List<CommentDto>> GetCommentsByUser(int UserId)
        {
            var UserData = await AuthenticationProvider.GetAuthenticationStateAsync();
            var token = UserData.User.FindFirst("UserToken").Value;

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"/api/Comment/ByUser?UserId={UserId}"
            );
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            return await response.Content.ReadFromJsonAsync<List<CommentDto>>();
        }
    }
}
