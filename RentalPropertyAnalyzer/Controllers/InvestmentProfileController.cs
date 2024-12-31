using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalPropertyAnalyzer.DataAccessLayer;

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
            // Assuming _context.InvestmentProfile correctly accesses your DbSet and
            // the stored procedure returns multiple profiles
            var profiles = await _context.InvestmentProfile
                                         .FromSqlRaw("EXEC GetInvestmentProfile")
                                         .ToListAsync();

            // No need to call FirstOrDefault() since you want a list of profiles
            // Directly pass the list of profiles to the view
            return View(profiles);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InvestmentProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvestmentProfileController/Create
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

        // GET: InvestmentProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InvestmentProfileController/Edit/5
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

        // GET: InvestmentProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InvestmentProfileController/Delete/5
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
