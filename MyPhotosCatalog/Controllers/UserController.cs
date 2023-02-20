using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPhotosCatalog.Models;
using MyPhotosCatalog.Models.ViewModels;
using MyPhotosCatalog.Repositories;

namespace MyPhotosCatalog.Controllers
{
    //Responsible for managing the users's views

    public class UserController : Controller
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public UserController(IRepository repository, ILogger<UserController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IActionResult HomePage()
        {
            _logger.LogInformation("A new entry to the website has been detected");
            return View(_repository.GetMostRatedPhotos());
        }

        public IActionResult Catalog(CatalogViewModel model)
        {
            //Creating a list of Select list items and saving to ViewBag and all of animals
            var categories = _repository.GetCategories();
            var items = new List<SelectListItem>
            {
                new SelectListItem { Text = "All", Value = "All", Selected = true }
            };
            foreach (var category in categories)
            {
                items.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });

            }
            ViewBag.Items = items;
            ViewBag.Photos = _repository.GetPhotos();
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int id1)
        {
            var photo = _repository.GetPhoto(id1);
            if (photo == null)
                return NotFound();
            ViewBag.Photo = photo;
            // the model of this view is Comment
            return View();
        }
        //Post method after new comment recieved
        [HttpPost]
        public IActionResult Details(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _repository.AddComment(comment);

                //Redirect to route to delete the last writen comment from the text field
                return RedirectToRoute(new
                {
                    controller = "user",
                    action = "Details",
                    id1 = comment.PhotoId
                });
            }
            var photo = _repository.GetPhoto(comment.PhotoId);
            ViewBag.Photo = photo;
            return View();

        }

    }
}
