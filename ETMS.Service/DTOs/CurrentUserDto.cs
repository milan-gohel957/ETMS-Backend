using System.Text.Json.Serialization;

namespace ETMS.Service.DTOs;

public class CurrentUserDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("firstname")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastname")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("profileUrl")]
    public string? ProfileUrl { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
}