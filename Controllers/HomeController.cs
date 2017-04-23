using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;

namespace OdeToFood.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new Restaurant { Id = 1, Name = "The House of Kobe." };

            //return new ObjectResult(model); // object result return serialized model, eg. in json (if app content in header is json. Depends on content header).
            return View();
        }
    }
}