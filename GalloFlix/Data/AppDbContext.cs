using GalloFlix.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GalloFlix.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Configuração do Muitos para Muitos do MovieGenre
        builder.Entity<MovieGenre>().HasKey(
            mg => new { mg.MovieId, mg.GenreId }
        );
        
        builder.Entity<MovieGenre>()
            .HasOne(mg => mg.Movie)
            .WithMany(m => m.Genres)
            .HasForeignKey(mg => mg.MovieId);
        
        builder.Entity<MovieGenre>()
            .HasOne(mg => mg.Genre)
            .WithMany(g => g.Movies)
            .HasForeignKey(mg => mg.GenreId);
        #endregion

        #region Popular usuário
        List<IdentityRole> roles = new()
        {
            new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Usuário",
                NormalizedName = "USUÁRIO"
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);

        List<IdentityUser> users = new()
        {
            new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "admin@galloflix.com",
                NormalizedEmail = "ADMIN@GALLOFLIX.COM",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                LockoutEnabled = false,
                EmailConfirmed = true
            },
            new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM",
                UserName = "User",
                NormalizedUserName = "USER",
                LockoutEnabled = true,
                EmailConfirmed = true,
            }
        };
        foreach (var user in users)
        {
            PasswordHasher<IdentityUser> pass = new();
            user.PasswordHash = pass.HashPassword(user, "@Etec123");
        }
        builder.Entity<IdentityUser>().HasData(users);

        List<AppUser> appUsers = new()
        {
            new AppUser()
            {
                AppUserId = users[0].Id,
                Name = "José Antonio Gallo Junior",
                Birthday = DateTime.Parse("05/08/1981"),
                Photo = "/img/usuarios/avatar.png"
            },
            new AppUser()
            {
                AppUserId = users[1].Id,
                Name = "Fulaninho",
                Birthday = DateTime.Parse("08/05/2000")
            },
        };
        builder.Entity<AppUser>().HasData(appUsers);

        List<IdentityUserRole<string>> userRoles = new()
        {
            new IdentityUserRole<string>()
            {
                UserId = users[0].Id,
                RoleId = roles[0].Id
            },
            new IdentityUserRole<string>()
            {
                UserId = users[0].Id,
                RoleId = roles[1].Id
            },
            new IdentityUserRole<string>()
            {
                UserId = users[1].Id,
                RoleId = roles[1].Id
            }
        };
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        #endregion

    }

}