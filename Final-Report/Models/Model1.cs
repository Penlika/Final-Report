using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Final_Report.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<ACCOUNT> ACCOUNT { get; set; }
        public virtual DbSet<BOOKINGFLIGHT> BOOKINGFLIGHT { get; set; }
        public virtual DbSet<BOOKINGHOTEL> BOOKINGHOTEL { get; set; }
        public virtual DbSet<BOOKINGPACKAGE> BOOKINGPACKAGE { get; set; }
        public virtual DbSet<COMMENTANDRATING> COMMENTANDRATING { get; set; }
        public virtual DbSet<FLIGHT> FLIGHT { get; set; }
        public virtual DbSet<HOTEL> HOTEL { get; set; }
        public virtual DbSet<LOGOIMG> LOGOIMG { get; set; }
        public virtual DbSet<PACKAGE> PACKAGE { get; set; }
        public virtual DbSet<ADMIN> ADMIN { get; set; }
        public virtual DbSet<CUSTOMER> CUSTOMER { get; set; }
        public virtual DbSet<MANAGER> MANAGER { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACCOUNT>()
                .HasOptional(e => e.ADMIN)
                .WithRequired(e => e.ACCOUNT);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.BOOKINGFLIGHT)
                .WithOptional(e => e.ACCOUNT)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.BOOKINGHOTEL)
                .WithOptional(e => e.ACCOUNT)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.BOOKINGPACKAGE)
                .WithOptional(e => e.ACCOUNT)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.COMMENTANDRATING)
                .WithOptional(e => e.ACCOUNT)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.CUSTOMER)
                .WithRequired(e => e.ACCOUNT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasOptional(e => e.MANAGER)
                .WithRequired(e => e.ACCOUNT);

            modelBuilder.Entity<FLIGHT>()
                .HasMany(e => e.BOOKINGFLIGHT)
                .WithOptional(e => e.FLIGHT)
                .HasForeignKey(e => e.IDFLIGHT);

            modelBuilder.Entity<FLIGHT>()
                .HasMany(e => e.PACKAGE)
                .WithOptional(e => e.FLIGHT)
                .HasForeignKey(e => e.IDFLIGHT);

            modelBuilder.Entity<HOTEL>()
                .HasMany(e => e.BOOKINGHOTEL)
                .WithOptional(e => e.HOTEL)
                .HasForeignKey(e => e.IDHOTEL);

            modelBuilder.Entity<HOTEL>()
                .HasMany(e => e.COMMENTANDRATING)
                .WithOptional(e => e.HOTEL)
                .HasForeignKey(e => e.IDHOTEL);

            modelBuilder.Entity<HOTEL>()
                .HasMany(e => e.PACKAGE)
                .WithOptional(e => e.HOTEL)
                .HasForeignKey(e => e.IDHOTEL);

            modelBuilder.Entity<PACKAGE>()
                .HasMany(e => e.BOOKINGPACKAGE)
                .WithOptional(e => e.PACKAGE)
                .HasForeignKey(e => e.IDPACKAGE);
        }
    }
}
