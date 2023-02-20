using Newtonsoft.Json;

namespace shareCalendar_api.Entities;

public class CalendarItemBlank
{
    [JsonProperty("id")]
    public Guid Id;
    [JsonProperty("createdOn")]
    public DateTimeOffset CreatedOn { get; set; }
    [JsonProperty("isShared")]
    public bool IsShared { get; set; }
    [JsonProperty("startsAt")]
    public DateTimeOffset StartsAt { get; set; }
    [JsonProperty("endsAt")]
    public DateTimeOffset EndsAt { get; set; }
    [JsonProperty("userId")]
    public Guid userId { get; set; }
    [JsonProperty("blank")]
    public bool Blank { get; set; }
}