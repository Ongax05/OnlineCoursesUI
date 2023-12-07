using System.Text.Json.Serialization;

namespace Dtos;

    public class DataUserDto
    {
    public int Id { get; set; }
    public string Message { get; set; }
    public bool IsAuthenticated { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    }
