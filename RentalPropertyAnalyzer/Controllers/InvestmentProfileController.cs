

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalPropertyAnalyzer.DataAccessLayer;
using RentalPropertyAnalyzer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalPropertyAnalyzer.Controllers
{
    public class InvestmentProfileController : Controller
    {
        private readonly InvestmentProfileContext _context;

        public InvestmentProfileController(InvestmentProfileContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch profiles from the database using the context
            var profiles = await _context.InvestmentProfile
                                         .FromSqlRaw("EXEC GetInvestmentProfile")
                                         .ToListAsync();
            return View(profiles);
        }

        // New method to handle updates to investment profiles
        [HttpPost]
        public async Task<IActionResult> SaveChanges(List<InvestmentProfile> profiles)
        {
            if (ModelState.IsValid)
            {
                // Iterate through each profile and update values in the database
                foreach (var profile in profiles)
                {
                    var existingProfile = await _context.InvestmentProfile.FindAsync(profile.Id);
                    if (existingProfile != null)
                    {
                        existingProfile.DownpaymentPercentage = profile.DownpaymentPercentage;
                        existingProfile.Term = profile.Term;
                        existingProfile.MortgageInterestRate = profile.MortgageInterestRate;
                        existingProfile.PropertyTaxRate = profile.PropertyTaxRate;
                        existingProfile.HomeownersInsurance = profile.HomeownersInsurance;
                        existingProfile.HOAEstimate = profile.HOAEstimate;
                        existingProfile.BalloonInsurance = profile.BalloonInsurance;
                        existingProfile.RealtorClosingFeePercentage = profile.RealtorClosingFeePercentage;
                        existingProfile.RenovationCosts = profile.RenovationCosts;
                        existingProfile.ClosingCosts = profile.ClosingCosts ;
                        existingProfile.PropertyManagementFee = profile.PropertyManagementFee;
                        existingProfile.LoanOriginationFee = profile.LoanOriginationFee;
                        existingProfile.AppraisalFee = profile.AppraisalFee;

                        existingProfile.CreditReportFee = profile.CreditReportFee;
                        existingProfile.TitleInsuranceCost = profile.TitleInsuranceCost;
                        existingProfile.TitleSearchFee = profile.TitleSearchFee;
                        existingProfile.EscrowFee = profile.EscrowFee;
                        existingProfile.FloodInspectionFee = profile.FloodInspectionFee;
                        existingProfile.MiscellaneousFees = profile.MiscellaneousFees;

                        existingProfile.PMIRate = profile.PMIRate;
                      
                    
                        existingProfile.OtherExpenses = profile.OtherExpenses;
                        existingProfile.AnnualAppreciationRate = profile.AnnualAppreciationRate;
                        existingProfile.VacancyRate = profile.VacancyRate;
                        existingProfile.MonthlyUtilitiesCost = profile.MonthlyUtilitiesCost;
                        existingProfile.HOAEstimate = profile.HOAEstimate;
                    }
                }

                // Save changes to the database
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Return the view with the updated profiles in case of a validation error
            return View("Index", profiles);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

