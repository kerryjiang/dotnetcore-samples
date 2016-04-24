using Microsoft.AspNetCore.Mvc;

namespace MvcIdentitySample.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {                
            return View();
        }
    }
}