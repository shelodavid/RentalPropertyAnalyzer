using Microsoft.EntityFrameworkCore;
using RentalPropertyAnalyzer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalPropertyAnalyzer.DataAccessLayer
{
    public class InvestmentProfileContext : DbContext
    {
        public InvestmentProfileContext(DbContextOptions<InvestmentProfileContext> options) : base(options)
        {
        }

        // DbSet for InvestmentProfile table
        public DbSet<InvestmentProfile> InvestmentProfile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly map the InvestmentProfile entity to the correct table in the database
            modelBuilder.Entity<InvestmentProfile>()
                .ToTable("InvestmentProfiles", schema: "dbo"); // **Updated:** Specify correct table name and schema

            // Configure the InvestmentProfile table
            modelBuilder.Entity<InvestmentProfile>(entity =>
            {
                entity.HasKey(e => e.Id); // Set primary key

                entity.Property(e => e.DownpaymentPercentage)
                    .HasColumnType("decimal(18,2)") // Ensure precision for decimal fields
                    .IsRequired();

                entity.Property(e => e.MortgageInterestRate)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.PropertyTaxRate)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                // Additional configurations can be added here if needed
            });
        }

        // Method to fetch InvestmentProfiles using a stored procedure
        public async Task<List<InvestmentProfile>> GetInvestmentProfilesAsync()
        {
            return await InvestmentProfile
                .FromSqlRaw("EXEC GetInvestmentProfile")
                .ToListAsync();
        }

        // Method to update InvestmentProfiles in the database
        public async Task UpdateInvestmentProfilesAsync(List<InvestmentProfile> profiles)
        {
            foreach (var profile in profiles)
            {
                var existingProfile = await InvestmentProfile.FindAsync(profile.Id);
                if (existingProfile != null)
                {
                    existingProfile.DownpaymentPercentage = profile.DownpaymentPercentage;
                    existingProfile.Term = profile.Term;
                    existingProfile.MortgageInterestRate = profile.MortgageInterestRate;
                    existingProfile.PropertyTaxRate = profile.PropertyTaxRate;
                    // Update other fields as necessary
                }
            }

            await SaveChangesAsync();
        }
    }
}
