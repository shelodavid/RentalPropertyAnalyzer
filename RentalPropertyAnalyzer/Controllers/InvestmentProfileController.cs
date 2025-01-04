//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using RentalPropertyAnalyzer.DataAccessLayer;

//namespace RentalPropertyAnalyzer.Controllers
//{
//    public class InvestmentProfileController : Controller
//    {
//        private readonly InvestmentProfileContext _context;

//        public InvestmentProfileController(InvestmentProfileContext context)
//        {
//            _context = context;
//        }


//        public async Task<IActionResult> Index()
//        {
//            // Assuming _context.InvestmentProfile correctly accesses your DbSet and
//            // the stored procedure returns multiple profiles
//            var profiles = await _context.InvestmentProfile
//                                         .FromSqlRaw("EXEC GetInvestmentProfile")
//                                         .ToListAsync();

//            // No need to call FirstOrDefault() since you want a list of profiles
//            // Directly pass the list of profiles to the view
//            return View(profiles);
//        }

//        public ActionResult Details(int id)
//        {
//            return View();
//        }

//        // GET: InvestmentProfileController/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: InvestmentProfileController/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: InvestmentProfileController/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: InvestmentProfileController/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: InvestmentProfileController/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: InvestmentProfileController/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Delete(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}

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
                        // Add additional field updates if necessary
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

