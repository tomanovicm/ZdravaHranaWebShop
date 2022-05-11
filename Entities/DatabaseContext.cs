using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZdravaHranaWebShop.Configurations;

namespace ZdravaHranaWebShop.Entities
{
    public class DatabaseContext : IdentityDbContext
    {
        

        public DatabaseContext(DbContextOptions options) : base(options)
        {
           
        }

        public DbSet<Adresa> Adresa { get; set; }
        public DbSet<Korpa> Korpa { get; set; }
        public DbSet<NacinPlacanja> NacinPlacanja { get; set; }
        public DbSet<Porudzbina> Porudzbina { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<Proizvodjac> Proizvodjac { get; set; }
        public DbSet<StavkaKorpe> StavkaKorpe { get; set; }
        public DbSet<TipProizvoda> TipProizvoda { get; set; }

        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());

            /*builder.Entity<User>()
               .HasData(new
               {
                   Id = "0617a204-9de5-4707-9a8d-0f7379ca6467",
                   Ime = "Milica",
                   Prezime = "Tomanovic",
                   Email = "tomanovicmilica@gmail.com",
                   Password = "milicat",
                   Roles = "Admin"

               });*/
        }
    }
}
