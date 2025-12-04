using Microsoft.AspNetCore.Mvc;

namespace CNPM5.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
