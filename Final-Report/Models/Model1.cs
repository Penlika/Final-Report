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
        public virtual DbSet<APPROVE> APPROVE { get; set; }
        public virtual DbSet<BOOKINGFLIGHT> BOOKINGFLIGHT { get; set; }
        public virtual DbSet<BOOKINGHOTEL> BOOKINGHOTEL { get; set; }
        public virtual DbSet<BOOKINGPACKAGE> BOOKINGPACKAGE { get; set; }
        public virtual DbSet<FLIGHT> FLIGHT { get; set; }
        public virtual DbSet<HOTEL> HOTEL { get; set; }
        public virtual DbSet<MANAGING> MANAGING { get; set; }
        public virtual DbSet<PAYING> PAYING { get; set; }
        public virtual DbSet<ADMIN> ADMIN { get; set; }
        public virtual DbSet<COMMENTANDRATING> COMMENTANDRATING { get; set; }
        public virtual DbSet<CUSTOMER> CUSTOMER { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.APPROVE)
                .WithOptional(e => e.ACCOUNT)
                .HasForeignKey(e => e.IDADMIN);

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
                .HasMany(e => e.MANAGING)
                .WithOptional(e => e.ACCOUNT)
                .HasForeignKey(e => e.IDADMIN);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.PAYING)
                .WithOptional(e => e.ACCOUNT)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<BOOKINGFLIGHT>()
                .HasMany(e => e.APPROVE)
                .WithOptional(e => e.BOOKINGFLIGHT)
                .HasForeignKey(e => e.IDFLIGHTBOOK);

            modelBuilder.Entity<BOOKINGFLIGHT>()
                .HasMany(e => e.PAYING)
                .WithOptional(e => e.BOOKINGFLIGHT)
                .HasForeignKey(e => e.IDFLIGHTBOOK);

            modelBuilder.Entity<BOOKINGHOTEL>()
                .HasMany(e => e.APPROVE)
                .WithOptional(e => e.BOOKINGHOTEL)
                .HasForeignKey(e => e.IDHOTELBOOK);

            modelBuilder.Entity<BOOKINGHOTEL>()
                .HasMany(e => e.PAYING)
                .WithOptional(e => e.BOOKINGHOTEL)
                .HasForeignKey(e => e.IDHOTELBOOK);

            modelBuilder.Entity<BOOKINGPACKAGE>()
                .HasMany(e => e.APPROVE)
                .WithOptional(e => e.BOOKINGPACKAGE)
                .HasForeignKey(e => e.IDPACKAGEBOOK);

            modelBuilder.Entity<BOOKINGPACKAGE>()
                .HasMany(e => e.PAYING)
                .WithOptional(e => e.BOOKINGPACKAGE)
                .HasForeignKey(e => e.IDPACKAGEBOOK);

            modelBuilder.Entity<FLIGHT>()
                .HasMany(e => e.BOOKINGFLIGHT)
                .WithOptional(e => e.FLIGHT)
                .HasForeignKey(e => e.IDFLIGHT);

            modelBuilder.Entity<FLIGHT>()
                .HasMany(e => e.BOOKINGPACKAGE)
                .WithOptional(e => e.FLIGHT)
                .HasForeignKey(e => e.IDFLIGHT);

            modelBuilder.Entity<FLIGHT>()
                .HasMany(e => e.MANAGING)
                .WithOptional(e => e.FLIGHT)
                .HasForeignKey(e => e.IDFLIGHT);

            modelBuilder.Entity<HOTEL>()
                .HasMany(e => e.BOOKINGHOTEL)
                .WithOptional(e => e.HOTEL)
                .HasForeignKey(e => e.IDHOTEL);

            modelBuilder.Entity<HOTEL>()
                .HasMany(e => e.BOOKINGPACKAGE)
                .WithOptional(e => e.HOTEL)
                .HasForeignKey(e => e.IDHOTEL);

            modelBuilder.Entity<HOTEL>()
                .HasMany(e => e.COMMENTANDRATING)
                .WithOptional(e => e.HOTEL)
                .HasForeignKey(e => e.IDHOTEL);

            modelBuilder.Entity<HOTEL>()
                .HasMany(e => e.MANAGING)
                .WithOptional(e => e.HOTEL)
                .HasForeignKey(e => e.IDHOTEL);

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.PHONENUMBER)
                .IsUnicode(false);
        }
    }
}
