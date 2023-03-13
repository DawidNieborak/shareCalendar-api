using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using shareCalendar_api.Entities;
using shareCalendar_api.Repositories;

namespace shareCalendar_api.Controllers;


[ApiController]
[Route("/api/v1/share")]
[EnableCors("_myAllowSpecificOrigins")]
public class BlankCalendarController : ControllerBase
{
    private readonly IBlankCalendarRepository blankCalendarRepository;
    private readonly IUserRepository userRepository;
    
    public BlankCalendarController(IBlankCalendarRepository blankCalendarRepository, IUserRepository userRepository)
    {
        this.blankCalendarRepository = blankCalendarRepository;
        this.userRepository = userRepository;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetUrlShareLink(Guid id)
    {
        var urlBase = "https://www.google.com/";

        var item = await userRepository.GetUserAsync(id);

        if (item is null)
        {
            return NotFound();    
        }

        return Ok(urlBase+item.Id);
    }

    [HttpGet]
    [Route("/getItems/{id}")]
    public async Task<ICollection<CalendarItemBlank>> GetAllBlankCalendarItems(Guid id)
    {
        // pass User Guid to get blank Calendar items;
        var items = await blankCalendarRepository.GetAllBlankCalendarItems(id);
        return items;
    }
    
}