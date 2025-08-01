using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ETMS.Service.DTOs;

public class CurrentUserDto
{


    [JsonPropertyName("firstname")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastname")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    [RegularExpression(@"^[a-zA-Z0-9._-]{3,15}$", ErrorMessage = "Username must be between 3 and 15 characters long and can only contain letters, numbers, underscores (_), hyphens (-), and periods (.).")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("profileUrl")]
    public string? ProfileUrl { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    public int? CreatedByUserId { get; set; }

}