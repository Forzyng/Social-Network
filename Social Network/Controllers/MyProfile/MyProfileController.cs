using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Network.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Network.Controllers.MyProfile
{
    [Authorize]
    public class MyProfileController : Controller
    {
        // GET: MyProfileController
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public MyProfileController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToPage("/Account/AccessDenied", new { Area = "Identity" });

            var userId = (await _userManager.GetUserAsync(User)).Id;

            var user = await _context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.Followers)
               .Include(u => u.Following)
               
               .FirstAsync();

           
            return View(user);
           // return View();
        }

        // GET: MyProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MyProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyProfileController/Create
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

        // GET: MyProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MyProfileController/Edit/5
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

        // GET: MyProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MyProfileController/Delete/5
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
