using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WorkshopsController : ControllerBase
{
    private readonly IApiService _apiService;

    public WorkshopsController(IApiService apiService)
    {
        _apiService = apiService;
    }

    [HttpPost]
    public async Task<ActionResult<Workshop>> CreateWorkshop(Workshop workshop)
    {
        var createdWorkshop = await _apiService.CreateWorkshopAsync(workshop);
        return CreatedAtAction(nameof(CreateWorkshop), new { id = createdWorkshop.Id }, createdWorkshop);
    }
}

