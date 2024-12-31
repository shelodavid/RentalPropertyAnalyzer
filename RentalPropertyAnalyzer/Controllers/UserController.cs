using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalPropertyAnalyzer.DataAccessLayer;
using RentalPropertyAnalyzer.Models.DBEntites;
using RentalPropertyAnalyzer.Services;

namespace RentalPropertyAnalyzer.Controllers
{
    public class UserController : Controller
    {
        private readonly UsersContext _context; // Replace with your data context
        private readonly PasswordHashService _passwordHashService; // If you're using a service for password hashing

        public UserController(UsersContext context, PasswordHashService passwordHashService)
        {
            _context = context;
            _passwordHashService = passwordHashService;
        }

        // GET: User/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Users model)
        {
            if (ModelState.IsValid)
            {
                byte[] passwordHash, passwordSalt;
                _passwordHashService.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

                var user = new Users
                {
                    Username = model.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                    // Other properties
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Redirect to login page or anywhere else after successful registration
                return RedirectToAction(nameof(Login));
            }

            return View(model);
        }

        // GET: User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Users model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username);

                if (user == null || !_passwordHashService.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                {
                    // Handle login failure
                    ModelState.AddModelError("", "Username or password is incorrect.");
                    return View(model);
                }

                // Handle successful login, e.g., setting up user session or cookie
                // ...

                return RedirectToAction("Index", "Home"); // Redirect to the home page or dashboard
            }

            return View(model);
        }

        // Other actions like Logout, ChangePassword, etc.
    }
}
