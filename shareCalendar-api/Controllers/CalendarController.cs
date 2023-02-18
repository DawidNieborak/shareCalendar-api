using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using shareCalendar_api.Entities;
using shareCalendar_api.Repositories;

namespace shareCalendar_api.Controllers;

[ApiController]
[Route("/api/v1/calendar")]
public class CalendarController : ControllerBase
{
    private readonly ICalendarRepository repository;


    public CalendarController(ICalendarRepository repository)
    {
        this.repository = repository;
    }

    #region endpoints

    [HttpGet("{id}")]
    public async Task<ActionResult<Calendar>> GetCalendarByIdAsync(Guid id)
    {
        var item = await repository.GetCalendarAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> PostCalendarAsync(CalendarItem calendarItem)
    {
        CalendarItem newCalendar = new()
        {
            Id = Guid.NewGuid(),
            Description = calendarItem.Description,
            Type = calendarItem.Type,
            Name = calendarItem.Name,
            StartsAt = calendarItem.StartsAt,
            EndsAt = calendarItem.EndsAt,
            CreatedOn = DateTimeOffset.Now,
            IsShared = calendarItem.IsShared,
            userId = calendarItem.userId
        };

        await repository.CreateCalendarAsync(newCalendar);

        return Ok(newCalendar);
    }
    #endregion
}