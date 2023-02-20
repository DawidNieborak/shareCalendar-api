using Newtonsoft.Json;

namespace shareCalendar_api.Entities;

public class UserPost
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    [JsonProperty("identityCode")]
    public string IdentityCode { get; set; }
    [JsonProperty("firstName")]
    public string FirstName { get; set; }
    [JsonProperty("secondName")]
    public string SecondName { get; set; }
}