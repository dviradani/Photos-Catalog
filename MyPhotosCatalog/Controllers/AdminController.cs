using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPhotosCatalog.Models;
using MyPhotosCatalog.Models.ViewModels;
using MyPhotosCatalog.Repositories;
using System.Collections.Generic;

namespace MyPhotosCatalog.Controllers
{
    //Responsible for managing the administrator's operations
    [Authorize]
    public class AdminController : Controller
    {
        IRepository _repository;
        public AdminController(IRepository repository)
        {
            _repository = repository;
        }
        // Admin`s Home Page
        public IActionResult Catalog(CatalogViewModel model)
        {
            LoadCategoriesToList(true);
            ViewBag.Photos = _repository.GetPhotos();
            return View(model);
        }

        //Deleting an animal from a database
        public IActionResult Delete(int id)
        {
            var animal = _repository.GetPhoto(id);
            if (animal == null)
                return Content("No animal was found");
            DeletePhotoFromServer(id);

            _repository.DeletePhoto(animal);
            return RedirectToAction("Catalog");
        }

        //Adding an animal to a database
        [HttpGet]
        public IActionResult Add()
        {
            //Creating a list of Select list items and saving to ViewBag
            LoadCategoriesToList(false);
            return View();
        }
        [HttpPost]
        public IActionResult Add(Photo animal)
        {
            // Adding Server-Side validation for photo within Add operation only
            if (animal.File == null)
            {
                ModelState.AddModelError("Photo", "Photo Is Required!");
            }
            if (!ModelState.IsValid)
            {
                LoadCategoriesToList(false);
                return View();
            }
            AddPhotoToServer(animal);
            _repository.InsertPhoto(animal);
            return RedirectToAction("Catalog");
        }

        //Editing an animal
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var animal = _repository.GetPhoto(id);
            if (animal == null)
                return NotFound();
            LoadCategoriesToList(false);
            return View(animal);
        }
        [HttpPost]
        public IActionResult Edit(Photo photo)
        {
            if (!ModelState.IsValid)
            {
                LoadCategoriesToList(false);
                return View(photo);
            }
            //switch photo in wwwroot
            if (photo.File != null)
            {
                DeletePhotoFromServer(photo.Id);
                AddPhotoToServer(photo);
            }
            _repository.UpdatePhoto(photo);
            return RedirectToAction("Catalog");
        }
        //Deleting an inappropriate comment (in the edit view)
        public IActionResult DeleteComment(int id)
        {
            var photo = _repository.FindPhotoByCommentId(id);
            if (photo == null)
            {
                return Content("No comment was found");
            }
            _repository.DeleteComment(id);
            return RedirectToRoute(
                new
                {
                    action = "Edit",
                    id = photo.Id
                });
        }
        //Creating a list of Select list items and saving to ViewBag
        private void LoadCategoriesToList(bool isInCatalogPage)
        {
            var categories = _repository.GetCategories();
            var items = new List<SelectListItem>();
            //"All" option should only be on the catalog page
            if (isInCatalogPage)
                items.Add(new SelectListItem { Text = "All", Value = "All", Selected = true });
            foreach (var category in categories)
            {
                items.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });

            }
            ViewBag.Items = items;
        }
        //uploads photo to wwwroot folder
        private void AddPhotoToServer(Photo photo)
        {
            var fileName = Guid.NewGuid().ToString() + "_" + photo.File!.FileName;
            string serverFolder = Path.Combine(Environment.CurrentDirectory, "wwwroot\\Pics\\", fileName);
            photo.File.CopyTo(new FileStream(serverFolder, FileMode.Create));
            photo.PictureName = fileName;
        }
        //deletes photo to wwwroot folder
        private void DeletePhotoFromServer(int id)
        {
            var animal = _repository.GetPhoto(id);
            var fullPath = Environment.CurrentDirectory + "\\wwwroot\\Pics\\" + animal.PictureName;

            if (System.IO.File.Exists(fullPath) && id > 8)
            {
                try
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    System.IO.File.Delete(fullPath);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
