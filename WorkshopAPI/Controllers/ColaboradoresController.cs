using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ColaboradoresController : ControllerBase
{
    private readonly IApiService _apiService;

    public ColaboradoresController(IApiService apiService)
    {
        _apiService = apiService;
    }

    [HttpPost]
    public async Task<ActionResult<Colaborador>> CreateColaborador(Colaborador colaborador)
    {
        var createdColaborador = await _apiService.CreateColaboradorAsync(colaborador);
        return CreatedAtAction(nameof(CreateColaborador), new { id = createdColaborador.Id }, createdColaborador);
    }

    [HttpGet]
    public async Task<ActionResult<List<Colaborador>>> GetColaboradoresWithWorkshops()
    {
        var colaboradores = await _apiService.GetColaboradoresWithWorkshopsAsync();
        return Ok(colaboradores);
    }
}

