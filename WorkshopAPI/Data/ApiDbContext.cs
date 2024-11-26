using Microsoft.EntityFrameworkCore;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<Workshop> Workshops { get; set; }
    public DbSet<Ata> Atas { get; set; }
}

