using Microsoft.EntityFrameworkCore;
using RentalPropertyAnalyzer.Models.DBEntites;

namespace RentalPropertyAnalyzer.DataAccessLayer
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
    }
}
