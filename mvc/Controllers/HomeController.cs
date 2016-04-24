
using Microsoft.AspNetCore.Mvc;

namespace HelloMvc
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