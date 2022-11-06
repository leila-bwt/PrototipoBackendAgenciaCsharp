
using BackendAgenciaCsharp.Model;
using Microsoft.EntityFrameworkCore;
namespace BackendAgenciaCsharp.Database
{
    public class UsuarioDbContext : DbContext
    {
      public UsuarioDbContext(DbContextOptions<UsuarioDbContext>
      options) : base(options){

      } 

      public DbSet<Usuario> Usuarios {get; set; } 

      protected override void OnModelCreating(ModelBuilder modelBuilder){
        var usuario = modelBuilder.Entity<Usuario>();
        usuario.ToTable("usuario");
        usuario.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
        usuario.Property(x => x.nome).HasColumnName("nome").IsRequired(); 
        usuario.Property(x => x.sobrenome).HasColumnName("sobrenome").IsRequired();
        usuario.Property(x => x.endereço).HasColumnName("endereço").IsRequired();
        usuario.Property(x => x.cidade).HasColumnName("cidade").IsRequired();
        usuario.Property(x => x.cep).HasColumnName("cep").IsRequired();
        usuario.Property(x => x.email).HasColumnName("email").IsRequired();
        usuario.Property(x => x.senha).HasColumnName("senha").IsRequired();
      }
       }
    }