using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using kalum2020_v1.Model;

namespace kalum2020_v1.DataContext
{
    public class KalumDbContext : DbContext
    {
        public DbSet<Alumno> Alumnos {get;set;}
        public DbSet<Instructor> Instructores {get;set;}
        public DbSet<Salon> Salones {get;set;}
        public DbSet<Horario> Horarios {get;set;}
        public DbSet<Religion> Religiones {get;set;}
        public DbSet<CarreraTecnica> CarrerasTecnicas {get;set;}
        public DbSet<Clase> Clases {get;set;}
        public DbSet<Usuario> UsuariosApp {get;set;}
        public DbSet<Rol> RolesApp {get;set;}
        public DbSet<UsuarioRol> UsuariosRoles {get;set;}
        public KalumDbContext(DbContextOptions<KalumDbContext> options)
        :base(options)
        {

        }

        public KalumDbContext()
        {

        }        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioRol>()
            .HasKey(x => new {x.UsuarioId, x.RoleId});
        }
    }
}