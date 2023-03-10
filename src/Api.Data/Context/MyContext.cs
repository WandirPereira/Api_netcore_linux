using Api.Data.Mapping;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UserEntity>? Users { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.UseGuidCollation(string.Empty);
            modelBuilder.Entity<UserEntity>(new UserMapping().Configure);

            //cria o usuário administrador - seed
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Administrador",
                    Email = "admin@mail.com",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                }
            );
        }
    }
}

