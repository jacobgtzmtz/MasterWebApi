
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence
{
    public class MasterNetDBContext : IdentityDbContext<AppUser>
    {
        //Crear cadena de conexion
        //Mapear Modelos a tablas
        public MasterNetDBContext(DbContextOptions<MasterNetDBContext> options) : base(options)
        {
        }

        public DbSet<Curso>? Cursos { get; set; }
        public DbSet<Instructor>? Instructores { get; set; }
        public DbSet<Precio>? Precios { get; set; }
        public DbSet<Calificacion>? Calificaciones { get; set; }
        public DbSet<Foto>? Fotos { get; set; }
        public DbSet<AppUser>? Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuraciones Fluent API
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().ToTable("usuarios");

            modelBuilder.Entity<Curso>().ToTable("cursos");
            modelBuilder.Entity<Instructor>().ToTable("instructores");
            modelBuilder.Entity<CursoInstructor>().ToTable("curso_instructores");
            modelBuilder.Entity<Precio>().ToTable("precios");
            modelBuilder.Entity<CursoPrecio>().ToTable("curso_precios");
            modelBuilder.Entity<Calificacion>().ToTable("calificaciones");
            modelBuilder.Entity<Foto>().ToTable("imagenes");

            modelBuilder.Entity<Precio>().Property(p => p.PrecioActual).HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Precio>().Property(p => p.PrecioPromocion).HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Precio>().Property(p => p.Nombre).HasMaxLength(100);

            modelBuilder.Entity<Curso>()
            .HasMany(c => c.Fotos)
            .WithOne(c => c.Curso)
            .HasForeignKey(c => c.CursoId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Curso>()
            .HasMany(c => c.Calificaciones)
            .WithOne(c => c.Curso)
            .HasForeignKey(c => c.CursoId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Curso>()
            .HasMany(c => c.Precios)
            .WithMany(c => c.Cursos)
            .UsingEntity<CursoPrecio>(
                j => j
                    .HasOne(cp => cp.Precio)
                    .WithMany(p => p.CursoPrecios)
                    .HasForeignKey(cp => cp.PrecioId),
                j => j
                    .HasOne(cp => cp.Curso)
                    .WithMany(c => c.CursoPrecios)
                    .HasForeignKey(cp => cp.CursoId),
                j => 
                { j.HasKey(t => new {t.PrecioId, t.CursoId}); }
            );   

            modelBuilder.Entity<Curso>()
            .HasMany(c => c.Instructores)
            .WithMany(i => i.Cursos)
            .UsingEntity<CursoInstructor>(
                j => j
                    .HasOne(ci => ci.Instructor)
                    .WithMany(i => i.CursoInstructores)
                    .HasForeignKey(ci => ci.InstructorId),
                j => j
                    .HasOne(c => c.Curso)
                    .WithMany(ci => ci.CursoInstructores)
                    .HasForeignKey(ci => ci.CursoId),
                j => 
                { j.HasKey(t => new {t.InstructorId, t.CursoId}); }
            );               
        } 
    }
}