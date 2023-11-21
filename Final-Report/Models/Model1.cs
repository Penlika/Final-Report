using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Final_Report.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<BOOKINGFLIGHT> BOOKINGFLIGHTs { get; set; }
        public virtual DbSet<BOOKINGHOTEL> BOOKINGHOTELs { get; set; }
        public virtual DbSet<BOOKINGPACKAGE> BOOKINGPACKAGEs { get; set; }
        public virtual DbSet<FLIGHT> FLIGHTs { get; set; }
        public virtual DbSet<HOTEL> HOTELs { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<ADMIN> ADMINs { get; set; }
        public virtual DbSet<COMMENTANDRATING> COMMENTANDRATINGs { get; set; }
        public virtual DbSet<CUSTOMER> CUSTOMERs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FLIGHT>()
                .HasMany(e => e.BOOKINGFLIGHTs)
                .WithOptional(e => e.FLIGHT)
                .HasForeignKey(e => e.IDFLIGHT);

            modelBuilder.Entity<FLIGHT>()
                .HasMany(e => e.BOOKINGPACKAGEs)
                .WithOptional(e => e.FLIGHT)
                .HasForeignKey(e => e.IDFLIGHT);

            modelBuilder.Entity<HOTEL>()
                .HasMany(e => e.BOOKINGHOTELs)
                .WithOptional(e => e.HOTEL)
                .HasForeignKey(e => e.IDHOTEL);

            modelBuilder.Entity<HOTEL>()
                .HasMany(e => e.BOOKINGPACKAGEs)
                .WithOptional(e => e.HOTEL)
                .HasForeignKey(e => e.IDHOTEL);

            modelBuilder.Entity<HOTEL>()
                .HasMany(e => e.COMMENTANDRATINGs)
                .WithOptional(e => e.HOTEL)
                .HasForeignKey(e => e.IDHOTEL);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.BOOKINGFLIGHTs)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.BOOKINGHOTELs)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.BOOKINGPACKAGEs)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.ADMINs)
                .WithRequired(e => e.USER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.COMMENTANDRATINGs)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.IDCUSTOMER);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.CUSTOMERs)
                .WithRequired(e => e.USER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.PHONENUMBER)
                .IsUnicode(false);
        }
    }
}
