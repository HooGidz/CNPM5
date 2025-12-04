using Microsoft.AspNetCore.Mvc;

namespace CNPM5.Controllers
{
    public class PersonalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
