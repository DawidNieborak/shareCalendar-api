using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using shareCalendar_api.Entities;
using shareCalendar_api.Repositories;

namespace shareCalendar_api.Controllers;

[ApiController]
[Route("/api/v1/calendar")]
[EnableCors("_myAllowSpecificOrigins")]
public class CalendarController : ControllerBase
{
    private readonly ICalendarRepository repository;
    private readonly IBlankCalendarRepository blankCalendarRepository;

    public CalendarController(ICalendarRepository repository, IBlankCalendarRepository blankCalendarRepository)
    {
        this.repository = repository;
        this.blankCalendarRepository = blankCalendarRepository;
    }

    #region endpoints

    [HttpGet("{id}")]
    public async Task<ActionResult<CalendarItem>> GetCalendarByIdAsync(Guid id)
    {
        var item = await repository.GetCalendarAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }
    
    [HttpPost]
    public async Task<ActionResult<CalendarItem>> PostCalendarAsync(CalendarItem calendarItem, Guid userId)
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
            userId = userId
        };

        if (calendarItem.IsShared)
        {
            CalendarItemBlank newCalendarItemBlank = new()
            {
                Id = Guid.NewGuid(),
                userId = userId,
                EndsAt = calendarItem.EndsAt,
                CreatedOn = DateTimeOffset.Now,
                IsShared = true,
                StartsAt = calendarItem.StartsAt,
                Blank = true,
            };

            await blankCalendarRepository.PostBlankCalendarItem(newCalendarItemBlank);
        }

        await repository.CreateCalendarAsync(newCalendar);

        return Ok(newCalendar);
    }


    [HttpDelete]
    public async Task<ActionResult> DeleteCalendarAsync(Guid id)
    {
        await repository.DeleteCalendarAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCalendarAsync(Guid id, CalendarItem updateItem)
    {
        var existingItem = await repository.GetCalendarAsync(id);

        if (existingItem is null)
        {
            return NotFound();
        }

        existingItem.Type = updateItem.Type;
        existingItem.Description = updateItem.Description;
        existingItem.Name = updateItem.Name;
        existingItem.IsShared = updateItem.IsShared;
        existingItem.EndsAt = updateItem.EndsAt;
        existingItem.StartsAt = updateItem.StartsAt;

        await repository.UpdateCalendarAsync(existingItem);
        return NoContent();
    }

    #endregion
}