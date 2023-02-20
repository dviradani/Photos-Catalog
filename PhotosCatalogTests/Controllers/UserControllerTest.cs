using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MyPhotosCatalog.Controllers;
using MyPhotosCatalog.Models;
using MyPhotosCatalog.Models.ViewModels;
using MyPhotosCatalog.Repositories;

namespace MyPhotosCatalog.Tests.Controllers
{
    //POC
    [TestClass]
    public class UserCotrollerTest
    {
        Mock<IRepository>? _repository;
        Mock<ILogger<UserController>>? _logger;
        UserController? _controller;
        List<Photo>? _photos;
        List<Comment>? _comments;
        List<Category>? _categories;
        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new Mock<IRepository>();
            _logger = new Mock<ILogger<UserController>>();
            _controller = new UserController(_repository.Object, _logger.Object);
            _photos = new List<Photo> { new Photo { Id = 1, Name = "Dog" }, new Photo() };
            _comments = new List<Comment> { new Comment(), new Comment() };
            _categories = new List<Category> { new Category(), new Category() };
        }

        // Checks whether a model has been transferred 
        [TestMethod]
        public void HomePage()
        {
            // Arrange
            _repository!.Setup(x => x.GetMostRatedPhotos()).Returns(GetMostRatedPhotos());
            UserController userController = new UserController(_repository.Object, _logger!.Object);

            // Act
            var result = userController.HomePage() as ViewResult;

            // Assert
            Assert.AreEqual(typeof(List<Photo>), result!.Model!.GetType());
            Assert.IsNotNull(result!.Model);
        }

        // Checks whether all data to view has been transferred 
        [TestMethod]
        public void Catalog()
        {
            //Arrange
            UserController userController = new UserController(_repository!.Object, _logger!.Object);
            //Act
            var result = userController.Catalog(new CatalogViewModel()) as ViewResult;

            //Assert
            Assert.IsNotNull(result!.ViewData["items"]);
            Assert.IsNotNull(result!.ViewData["Photos"]);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(CatalogViewModel));

        }
        //Test if right page showed (404)
        [TestMethod]
        public void Details_ReturnsNotFound_WhenPhotoIsNull()
        {
            //Arrange
            int id = -5;
            _repository!.Setup(x => x.GetPhoto(id)).Returns(GetPhoto(id));
            // Act
            var result = _controller!.Details(id) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
        //Test if right page showed (View)
        [TestMethod]
        public void Details_ReturnsViewResult_WhenPhotoIsNotNull()
        {
            // Arrange
            int id = 1;
            _repository!.Setup(x => x.GetPhoto(id)).Returns(GetPhoto(id));

            // Act
            var result = _controller!.Details(id) as ViewResult;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData["Photo"]);
        }

        private Photo GetPhoto(int id)
        {
            return _photos!.FirstOrDefault(x => x.Id == id)!;
        }

        private IEnumerable<Photo> GetMostRatedPhotos()
        {
            return _photos!.OrderByDescending(a => a.Name).Take(2).ToList();
        }
    }
}

