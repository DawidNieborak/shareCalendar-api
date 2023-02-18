using Microsoft.AspNetCore.Mvc;
using shareCalendar_api.Entities;
using shareCalendar_api.Repositories;

namespace shareCalendar_api.Controllers;

[ApiController]
[Route("/api/v1/user")]
public class UserController : ControllerBase
{
    private readonly IUserRepository repository;
    
    public UserController(IUserRepository repository)
    {
        this.repository = repository;
    }

    #region endpoints

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserByIdAsync(Guid id)
    {
        var item = await repository.GetUserAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return item;
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> PostUserAsync(User user)
    {
        User newUser = new()
        {
            Id = Guid.NewGuid(),
            IdentityCode = "null",
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            CalendarItem = user.CalendarItem
        };
        
        var identityCodeGen = new Random().Next();
        
        newUser.IdentityCode.Replace("null", identityCodeGen.ToString());
        await repository.CreateUserAsync(newUser);

        return Ok(new {
            newUser
        });
    }
    
    

    #endregion
    
}