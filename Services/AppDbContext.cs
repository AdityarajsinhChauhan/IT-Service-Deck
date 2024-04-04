using IT_Service_Deck.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace IT_Service_Deck.Services
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Requests> Requests { get; set; }

        public DbSet<RequestRouting> RequestRouting { get; set; }

        public DbSet<RequestHistory> RequestHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Requests>().ToTable("Requests");

            builder.Entity<RequestRouting>().ToTable("RequestRouting");

            builder.Entity<RequestHistory>().ToTable("RequestHistory");



            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var employee = new IdentityRole("employee");
            employee.NormalizedName = "employee";

            var approver = new IdentityRole("approver");
            approver.NormalizedName = "approver";

            builder.Entity<IdentityRole>().HasData(admin, employee, approver);

            builder.Entity<Requests>()
        .HasOne(r => r.Employee)
        .WithMany()
        .HasForeignKey(r => r.E_id)
        .IsRequired();


            builder.Entity<RequestHistory>()
       .HasOne(rh => rh.Requests)
       .WithMany()
       .HasForeignKey(rh => rh.RequestId); // This specifies the foreign key column in the RequestHistory table

            base.OnModelCreating(builder);
        }
    }
}
