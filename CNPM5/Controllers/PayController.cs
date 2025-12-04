using Microsoft.AspNetCore.Mvc;

namespace CNPM5.Controllers
{
    public class PayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
