using Microsoft.AspNetCore.Mvc;
using shareCalendar_api.Entities;

namespace shareCalendar_api.Repositories;

public interface IBlankCalendarRepository
{
    Task<ICollection<CalendarItemBlank>> GetAllBlankCalendarItems(Guid id);
    Task PostBlankCalendarItem(CalendarItemBlank calendarItemBlank);

}