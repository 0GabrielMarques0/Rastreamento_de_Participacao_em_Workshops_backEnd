public interface IApiService
{
    Task<Workshop> CreateWorkshopAsync(Workshop workshop);
    Task<Colaborador> CreateColaboradorAsync(Colaborador colaborador);
    Task<Ata> CreateAtaAsync(int workshopId);
    Task AddColaboradorToAtaAsync(int ataId, int colaboradorId);
    Task RemoveColaboradorFromAtaAsync(int ataId, int colaboradorId);
    Task<List<Colaborador>> GetColaboradoresWithWorkshopsAsync();
    Task<List<Colaborador>> GetColaboradoresByWorkshopNameAsync(string workshopNome);
    Task<List<Colaborador>> GetColaboradoresByWorkshopDateAsync(DateTime data);
}

