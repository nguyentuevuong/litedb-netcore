using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiteDb.AspNetCore.Auth.Models;
using LiteDb.AspNetCore.Data;
using LiteDB;
using LiteDb.AspNetCore.Identity.Models;

namespace LiteDb.AspNetCore.Auth.Controllers
{
    public class HomeController : Controller
    {
        private LiteRepository repo;
        public HomeController(LiteDbContext context)
        {
            repo = context.LiteRepository;
        }

        public IActionResult Index()
        {
            long a = repo.Query<ApplicationUser>().Count();

            return View(repo.Query<ApplicationUser>().Skip(100).Limit(20000).ToEnumerable().OrderBy(x => x.NormalizedEmail));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
