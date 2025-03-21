using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RotaViagem.Domain;
using RotaViagem.Domain.Identity;

namespace RotaViagem.Repository.Contextos
{
    public class RotaViagemContext : IdentityDbContext<User, Role, int, 
                                                       IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
                                                       IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public RotaViagemContext(DbContextOptions<RotaViagemContext> options) 
            : base(options) { }

        public DbSet<Local> Locals { get; set; }
        public DbSet<Rota> Rotas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole => 
                {
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId});

                    userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                    userRole.HasOne(ur => ur.User)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
                }
            );

            modelBuilder.Entity<Local>()
                .HasMany(e => e.RotaOrigems)
                .WithOne(rs => rs.LocalOrigem)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Local>()
                .HasMany(e => e.RotaDestinos)
                .WithOne(rs => rs.LocalDestino)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}