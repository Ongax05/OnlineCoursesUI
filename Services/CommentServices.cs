using System.Net.Http.Json;
using Dtos;
using OnlineCoursesUI.Interfaces;

namespace OnlineCoursesUI.Services
{
    public class CommentServices (HttpClient httpClient) : ICommentServices
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<bool> PostComment(CommentDto commentDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Comment", commentDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CommentDto>> GetCommentsByCourse(int CourseId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<CommentDto>>($"api/Comment/ByCourse?CourseId={CourseId}");
            return response;
        }

        public async Task<List<CommentDto>> GetCommentsByUser(int UserId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<CommentDto>>($"/api/Comment/ByUser?UserId={UserId}");
            return response;
        }
    }
}