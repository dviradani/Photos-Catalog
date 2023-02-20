using MyPhotosCatalog.DAL;
using MyPhotosCatalog.Models;

namespace MyPhotosCatalog.Repositories
{
    public class Repository : IRepository
    {
        private readonly PhotosCatalogContext _context;

        public Repository(PhotosCatalogContext context)
        {
            _context = context;
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void DeletePhoto(Photo photo)
        {
            _context.Photos.Remove(photo);
            _context.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            var comment = _context.Comments.Find(id);
            _context.Comments.Remove(comment!);
            _context.SaveChanges();
        }

        public Photo FindPhotoByCommentId(int commentId)
        {
            var comment = _context.Comments.Find(commentId);
            if (comment == null)
            {
                return null!;
            }
            var photo = _context.Photos.Find(comment!.PhotoId);
            return photo!;
        }

        public Photo GetPhoto(int id)
        {
            return _context.Photos.Find(id)!;
        }

        public IEnumerable<Photo> GetPhotos()
        {
            return _context.Photos.OrderBy(photo => photo.Name).ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories;
        }

        public IEnumerable<Photo> GetMostRatedPhotos()
        {
            var photos = _context.Photos.OrderByDescending(photo => photo.Comments!.Count())
                .Take(9)
                .ToList();
            return photos;
        }

        public void InsertPhoto(Photo photo)
        {
            _context.Add(photo);
            _context.SaveChanges();
        }

        public void UpdatePhoto(Photo photo)
        {
            var photoBeforeUpdate = _context.Photos.Find(photo.Id);
            photoBeforeUpdate!.Name = photo.Name;
            photoBeforeUpdate.PictureName = photo.PictureName;
            photoBeforeUpdate.Owner = photo.Owner;
            photoBeforeUpdate.CategoryId = photo.CategoryId;
            _context.SaveChanges();
        }
    }
}
