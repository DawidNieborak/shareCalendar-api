using System.Globalization;
using shareCalendar_api.Entities;

namespace shareCalendar_api.Repositories;

public interface ICalendarRepository
{
    Task<CalendarItem> GetCalendarAsync(Guid id);
    Task<ICollection<CalendarItem>> GetAllCalendarsAsync(Guid id);
    Task CreateCalendarAsync(CalendarItem calendar);
    Task UpdateCalendarAsync(CalendarItem calendar);
    Task DeleteCalendarAsync(Guid id);
}