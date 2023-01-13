using Microsoft.EntityFrameworkCore;
using ProyectoBanco.Core.Entidades;
using System.Reflection;

namespace ProyectoBanco.Infrastructure.Data;

public partial class BancoInterandinoDbContext : DbContext
{
    public BancoInterandinoDbContext()
    {
    }

    public BancoInterandinoDbContext(DbContextOptions<BancoInterandinoDbContext> opciones)
        : base(opciones)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Seguridad> Seguridades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BancoInterandinoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
