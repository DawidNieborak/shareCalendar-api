
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace shareCalendar_api.Entities;

public class User
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    [JsonProperty("identityCode")]
    public string IdentityCode { get; set; }
    [JsonProperty("firstName")]
    public string FirstName { get; set; }
    [JsonProperty("secondName")]
    public string SecondName { get; set; }
    [JsonProperty("calendarItem")]
    public ICollection<CalendarItem>? CalendarItem { get; set; }
}