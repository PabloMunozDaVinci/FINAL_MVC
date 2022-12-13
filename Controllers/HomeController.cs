using FINAL_MVC.Data;
using FINAL_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FINAL_MVC.Controllers
{


  
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    namespace FINAL_MVC.Controllers
    {
        public class HomeController : Controller
        {
            private readonly Context _context;

            public HomeController(Context context)
            {
                _context = context;
            }


            public IActionResult Index()
            {
                return View();
            }


           
            // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            //public IActionResult Error()
            //{
            //  return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            //}
        }
    }
}
