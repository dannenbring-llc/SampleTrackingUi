using Microsoft.AspNetCore.Mvc;

namespace SampleTrackingUi.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult TrayMap()
        {
            return View();
        }
    }
}