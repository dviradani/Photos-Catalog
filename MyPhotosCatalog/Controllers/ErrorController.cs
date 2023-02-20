using Microsoft.AspNetCore.Mvc;

namespace MyPhotosCatalog.Controllers
{
    public class ErrorController : Controller
    {
        //default view for unhandled exceptions
        public IActionResult Index()
        {
            return Content("Unknown Error");
        }
    }
}
