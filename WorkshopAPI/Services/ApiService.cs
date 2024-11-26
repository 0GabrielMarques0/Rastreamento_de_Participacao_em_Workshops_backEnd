using Microsoft.EntityFrameworkCore;

public class ApiService : IApiService
{
    private readonly ApiDbContext _context;

    public ApiService(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<Workshop> CreateWorkshopAsync(Workshop workshop)
    {
        _context.Workshops.Add(workshop);
        await _context.SaveChangesAsync();
        return workshop;
    }

    public async Task<Colaborador> CreateColaboradorAsync(Colaborador colaborador)
    {
        _context.Colaboradores.Add(colaborador);
        await _context.SaveChangesAsync();
        return colaborador;
    }

    public async Task<Ata> CreateAtaAsync(int workshopId)
    {
        var workshop = await _context.Workshops.FindAsync(workshopId);
        if (workshop == null)
        {
            throw new ArgumentException("Workshop not found");
        }

        var ata = new Ata { Workshop = workshop };
        _context.Atas.Add(ata);
        await _context.SaveChangesAsync();
        return ata;
    }

    public async Task AddColaboradorToAtaAsync(int ataId, int colaboradorId)
    {
        var ata = await _context.Atas.Include(a => a.Colaboradores).FirstOrDefaultAsync(a => a.Id == ataId);
        var colaborador = await _context.Colaboradores.FindAsync(colaboradorId);

        if (ata == null || colaborador == null)
        {
            throw new ArgumentException("Ata or Colaborador not found");
        }

        ata.Colaboradores.Add(colaborador);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveColaboradorFromAtaAsync(int ataId, int colaboradorId)
    {
        var ata = await _context.Atas.Include(a => a.Colaboradores).FirstOrDefaultAsync(a => a.Id == ataId);
        var colaborador = await _context.Colaboradores.FindAsync(colaboradorId);

        if (ata == null || colaborador == null)
        {
            throw new ArgumentException("Ata or Colaborador not found");
        }

        ata.Colaboradores.Remove(colaborador);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Colaborador>> GetColaboradoresWithWorkshopsAsync()
    {
        return await _context.Colaboradores
            .Include(c => c.Atas)
            .ThenInclude(a => a.Workshop)
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<List<Colaborador>> GetColaboradoresByWorkshopNameAsync(string workshopNome)
    {
        return await _context.Atas
            .Where(a => a.Workshop.Nome.Contains(workshopNome))
            .SelectMany(a => a.Colaboradores)
            .Distinct()
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<List<Colaborador>> GetColaboradoresByWorkshopDateAsync(DateTime data)
    {
        return await _context.Atas
            .Where(a => a.Workshop.DataRealizacao.Date == data.Date)
            .SelectMany(a => a.Colaboradores)
            .Distinct()
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }
}

