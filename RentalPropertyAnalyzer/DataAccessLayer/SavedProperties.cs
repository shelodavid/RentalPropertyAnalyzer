using Microsoft.EntityFrameworkCore;
using RentalPropertyAnalyzer.Models;

namespace RentalPropertyAnalyzer.DataAccessLayer
{
    public class SavedPropertiesContext : DbContext
    {
        public SavedPropertiesContext(DbContextOptions<SavedPropertiesContext> options) : base(options)
        {
        }

        public DbSet<SavedProperties> SavedProperties { get; set; }
    }

}
