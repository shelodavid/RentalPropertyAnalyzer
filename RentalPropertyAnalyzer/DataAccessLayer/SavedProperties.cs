//using Microsoft.EntityFrameworkCore;
//using RentalPropertyAnalyzer.Models;

//namespace RentalPropertyAnalyzer.DataAccessLayer
//{
//    public class SavedPropertiesContext : DbContext
//    {
//        public SavedPropertiesContext(DbContextOptions<SavedPropertiesContext> options) : base(options)
//        {
//        }

//        public DbSet<SavedProperties> SavedProperties { get; set; }
//    }

//}

using Microsoft.EntityFrameworkCore;
using RentalPropertyAnalyzer.Models;

namespace RentalPropertyAnalyzer.DataAccessLayer
{
    public class SavedPropertiesContext : DbContext
    {
        public SavedPropertiesContext(DbContextOptions<SavedPropertiesContext> options) : base(options)
        {
        }

        // DbSet for the SavedProperties entity (represents the SavedPropertyListings table)
        public DbSet<SavedProperties> SavedProperties { get; set; }

        // DbSet for the PurchaseSheetViewModel (represents results from GetPurchaseSheet stored procedure)
        public DbSet<PurchaseSheetViewModel> PurchaseSheetResults { get; set; }

        // DbSet for the ForecastBaseRow (represents results from GetForecastBase stored procedure)
        public DbSet<ForecastBaseRow> ForecastBaseRows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure SavedProperties mapping (if needed, for explicit table mapping)
            modelBuilder.Entity<SavedProperties>()
                .ToTable("SavedPropertyListings", "dbo"); // Maps to dbo.SavedPropertyListings

            // Configure PurchaseSheetViewModel as a keyless entity
            modelBuilder.Entity<PurchaseSheetViewModel>().HasNoKey();

            modelBuilder.Entity<ForecastBaseRow>()
                .HasNoKey()
                .ToView(null);

            // Optional: If additional mappings or constraints are required, add them here
        }
    }
}
