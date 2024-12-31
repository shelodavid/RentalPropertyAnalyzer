using Microsoft.EntityFrameworkCore;
using RentalPropertyAnalyzer.Models.DBEntites;

namespace RentalPropertyAnalyzer.DataAccessLayer
{
    public class RentalListingContext : DbContext

    {
        public RentalListingContext(DbContextOptions<RentalListingContext> options) : base(options)
        {

        }

        public DbSet<RentalListings>RentalListings { get; set; }
        

    }
}
