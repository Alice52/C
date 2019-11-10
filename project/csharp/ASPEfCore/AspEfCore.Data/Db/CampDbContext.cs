using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AspEfCore.Data.Models
{
    public partial class CampDbContext : DbContext
    {
        public CampDbContext()
        {
        }

        public CampDbContext(DbContextOptions<CampDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<CityCompany> CityCompany { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Major> Major { get; set; }
        public virtual DbSet<Province> Province { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=demo");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(e => e.ProvinceId)
                    .HasName("IX_Cities_ProvinceId");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Cities_Provinces_ProvinceId");
            });

            modelBuilder.Entity<CityCompany>(entity =>
            {
                entity.HasKey(e => new { e.CityId, e.CompanyId })
                    .HasName("PK_CityCompanies");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("IX_CityCompanies_CompanyId");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.CityCompany)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_CityCompanies_Cities_CityId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CityCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_CityCompanies_Companies_CompanyId");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.HasIndex(e => e.CityId)
                    .HasName("IX_Majors_CityId")
                    .IsUnique();

                entity.HasOne(d => d.City)
                    .WithOne(p => p.Major)
                    .HasForeignKey<Major>(d => d.CityId)
                    .HasConstraintName("FK_Majors_Cities_CityId");
            });

            // seed data: should privide Primary Key
            modelBuilder.Entity<City>().HasData(
                // new City() { Id = 1, Name = "BeiJing", AreaCode = "1024", Major= new Major() { Id = 1, FirstName = "zack" } }); //error
                new City() { Id = 1, Name = "BeiJing", AreaCode = "1024" });

            modelBuilder.Entity<Major>().HasData(
                new Major() { Id = 1, FirstName = "zack", CityId = 1 },
                new { Id =2, FirstName = "zack", CityId = 1 } // if no PK Id
               );
        }
    }
}
