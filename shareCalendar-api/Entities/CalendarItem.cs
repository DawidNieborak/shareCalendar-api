
using Newtonsoft.Json;
using shareCalendar_api.Entities.Enums;

namespace shareCalendar_api.Entities;

public class CalendarItem
{
    [JsonProperty("id")]
    public Guid Id;
    [JsonProperty("createdOn")]
    public DateTimeOffset CreatedOn { get; set; }
    [JsonProperty("description")]
    public string? Description { get; set; }
    [JsonProperty("isShared")]
    public bool IsShared { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("startsAt")]
    public DateTimeOffset StartsAt { get; set; }
    [JsonProperty("endsAt")]
    public DateTimeOffset EndsAt { get; set; }
    [JsonProperty("type")]
    public CalendarTypes? Type { get; set; }
    [JsonProperty("userId")]
    public Guid userId { get; set; }
}