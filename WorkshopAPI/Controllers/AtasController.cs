using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AtasController : ControllerBase
{
    private readonly IApiService _apiService;

    public AtasController(IApiService apiService)
    {
        _apiService = apiService;
    }

    [HttpPost]
    public async Task<ActionResult<Ata>> CreateAta(int workshopId)
    {
        var createdAta = await _apiService.CreateAtaAsync(workshopId);
        return CreatedAtAction(nameof(CreateAta), new { id = createdAta.Id }, createdAta);
    }

    [HttpPut("{ataId}/colaboradores/{colaboradorId}")]
    public async Task<IActionResult> AddColaboradorToAta(int ataId, int colaboradorId)
    {
        await _apiService.AddColaboradorToAtaAsync(ataId, colaboradorId);
        return NoContent();
    }

    [HttpDelete("{ataId}/colaboradores/{colaboradorId}")]
    public async Task<IActionResult> RemoveColaboradorFromAta(int ataId, int colaboradorId)
    {
        await _apiService.RemoveColaboradorFromAtaAsync(ataId, colaboradorId);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<List<Colaborador>>> GetColaboradoresByWorkshop([FromQuery] string workshopNome, [FromQuery] DateTime? data)
    {
        if (!string.IsNullOrEmpty(workshopNome))
        {
            var colaboradoresByName = await _apiService.GetColaboradoresByWorkshopNameAsync(workshopNome);
            return Ok(colaboradoresByName);
        }
        else if (data.HasValue)
        {
            var colaboradoresByDate = await _apiService.GetColaboradoresByWorkshopDateAsync(data.Value);
            return Ok(colaboradoresByDate);
        }
        else
        {
            return BadRequest("Please provide either workshopNome or data parameter");
        }
    }
}

