using Microsoft.EntityFrameworkCore;
using RentalPropertyAnalyzer.Models;
using System.Collections.Generic;
namespace RentalPropertyAnalyzer.DataAccessLayer

{
    public class InvestmentProfileContext: DbContext
    {
        public InvestmentProfileContext(DbContextOptions<InvestmentProfileContext> options) : base(options) 
        {
        }
        public DbSet<InvestmentProfile> InvestmentProfile { get; set; }
    }

   
}
