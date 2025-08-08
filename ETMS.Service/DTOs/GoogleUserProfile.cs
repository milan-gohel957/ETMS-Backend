using Newtonsoft.Json;

namespace ETMS.Service.DTOs;

public class GoogleUserProfile
{
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Picture { get; set; } = default!;
    public string GivenName { get; set; } = default!;
    public string FamilyName { get; set; } = default!;

    [JsonProperty("verified_email")]
    public bool VerifiedEmail { get; set; } = default!;
}