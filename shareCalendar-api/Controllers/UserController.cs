using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using shareCalendar_api.Entities;
using shareCalendar_api.Repositories;

namespace shareCalendar_api.Controllers;

[ApiController]
[Route("/api/v1/user")]
[EnableCors("_myAllowSpecificOrigins")]
public class UserController : ControllerBase
{
    private readonly IUserRepository repository;
    private readonly ICalendarRepository calendarRepository;

    public UserController(IUserRepository repository, ICalendarRepository calendarRepository)
    {
        this.repository = repository;
        this.calendarRepository = calendarRepository;
    }

    #region endpoints

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserByIdAsync(Guid id)
    {
        var item = await repository.GetUserAsync(id);

        var calendarItems = await calendarRepository.GetAllCalendarsAsync(id);
        
        if (item is null)
        {
            return NotFound();
        }

        User updatedUser = new()
        {
            Id = item.Id,
            FirstName = item.FirstName,
            SecondName = item.SecondName,
            IdentityCode = item.IdentityCode,
            CalendarItem = calendarItems
        };

        await repository.UpdateUserAsync(updatedUser);
        
        return updatedUser;
    }
    
    [HttpPost]
    public async Task<ActionResult> PostUserAsync(UserPost userPost)
    {
        User newUser = new()
        {
            Id = Guid.NewGuid(),
            IdentityCode = userPost.IdentityCode,
            FirstName = userPost.FirstName,
            SecondName = userPost.SecondName,
            CalendarItem = null
        };

        await repository.CreateUserAsync(newUser);
        return Ok(newUser);
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteUserAsync(Guid id)
    {
        await repository.DeleteUserAsync(id);
        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUserAsync(Guid id, UserPost updateItem)
    {
        var existingItem = await repository.GetUserAsync(id);

        if (existingItem is null)
        {
            return NotFound();
        }

        existingItem.FirstName = updateItem.FirstName;
        existingItem.SecondName = updateItem.SecondName;
        existingItem.IdentityCode = updateItem.IdentityCode;

        await repository.UpdateUserAsync(existingItem);
        return NoContent();
    }

    #endregion
    
}