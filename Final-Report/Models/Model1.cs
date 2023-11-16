using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FundamentalProject.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<ADMIN> ADMINs { get; set; }
        public virtual DbSet<CUSTOMER> CUSTOMERs { get; set; }
        public virtual DbSet<FLIGHT> FLIGHTs { get; set; }
        public virtual DbSet<HOTEL> HOTELs { get; set; }
        public virtual DbSet<PACKAGE> PACKAGEs { get; set; }
        public virtual DbSet<BOOKINGFLIGHT> BOOKINGFLIGHTs { get; set; }
        public virtual DbSet<BOOKINGHOTEL> BOOKINGHOTELs { get; set; }
        public virtual DbSet<BOOKINGPACKAGE> BOOKINGPACKAGEs { get; set; }
        public virtual DbSet<COMMENTANDRATING> COMMENTANDRATINGs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ADMIN>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.PHONENUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<CUSTOMER>()
                .HasMany(e => e.BOOKINGFLIGHTs)
                .WithOptional(e => e.CUSTOMER)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<CUSTOMER>()
                .HasMany(e => e.BOOKINGHOTELs)
                .WithOptional(e => e.CUSTOMER)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<CUSTOMER>()
                .HasMany(e => e.BOOKINGPACKAGEs)
                .WithOptional(e => e.CUSTOMER)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<CUSTOMER>()
                .HasMany(e => e.COMMENTANDRATINGs)
                .WithOptional(e => e.CUSTOMER)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<FLIGHT>()
                .HasMany(e => e.BOOKINGFLIGHTs)
                .WithOptional(e => e.FLIGHT)
                .HasForeignKey(e => e.IDFLIGHT);

            modelBuilder.Entity<FLIGHT>()
                .HasMany(e => e.PACKAGEs)
                .WithOptional(e => e.FLIGHT)
                .HasForeignKey(e => e.IDFLIGHT);

            modelBuilder.Entity<HOTEL>()
                .HasMany(e => e.BOOKINGHOTELs)
                .WithOptional(e => e.HOTEL)
                .HasForeignKey(e => e.IDHOTEL);

            modelBuilder.Entity<HOTEL>()
                .HasMany(e => e.PACKAGEs)
                .WithOptional(e => e.HOTEL)
                .HasForeignKey(e => e.IDHOTEL);

            modelBuilder.Entity<PACKAGE>()
                .HasMany(e => e.BOOKINGPACKAGEs)
                .WithOptional(e => e.PACKAGE)
                .HasForeignKey(e => e.IDPACKAGE);

            modelBuilder.Entity<PACKAGE>()
                .HasMany(e => e.COMMENTANDRATINGs)
                .WithOptional(e => e.PACKAGE)
                .HasForeignKey(e => e.IDPACKAGES);
        }
    }
}
