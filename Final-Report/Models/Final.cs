using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Final_Report.Models
{
    public partial class Final : DbContext
    {
        public Final()
            : base("name=Final")
        {
        }

        public virtual DbSet<BOOKING_FORM> BOOKING_FORM { get; set; }
        public virtual DbSet<FLIGHT> FLIGHTS { get; set; }
        public virtual DbSet<HOTEL> HOTELS { get; set; }
        public virtual DbSet<PACKAGE> PACKAGES { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<ADMIN> ADMINs { get; set; }
        public virtual DbSet<COMMENTANDRATING> COMMENTANDRATINGs { get; set; }
        public virtual DbSet<CUSTOMER> CUSTOMERs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FLIGHT>()
                .Property(e => e.PRICE_PER_PERSON)
                .HasPrecision(18, 0);

            modelBuilder.Entity<FLIGHT>()
                .HasMany(e => e.PACKAGES)
                .WithOptional(e => e.FLIGHT)
                .HasForeignKey(e => e.IDFLIGHT);

            modelBuilder.Entity<HOTEL>()
                .HasMany(e => e.PACKAGES)
                .WithOptional(e => e.HOTEL)
                .HasForeignKey(e => e.IDHOTEL);

            modelBuilder.Entity<PACKAGE>()
                .HasMany(e => e.COMMENTANDRATINGs)
                .WithOptional(e => e.PACKAGE)
                .HasForeignKey(e => e.IDPACKAGES);

            modelBuilder.Entity<PACKAGE>()
                .HasMany(e => e.CUSTOMERs)
                .WithOptional(e => e.PACKAGE)
                .HasForeignKey(e => e.BOOKINGLISTS);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.BOOKING_FORM)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.COMMENTANDRATINGs)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.CUSTOMERs)
                .WithRequired(e => e.USER)
                .WillCascadeOnDelete(false);
        }
    }
}
