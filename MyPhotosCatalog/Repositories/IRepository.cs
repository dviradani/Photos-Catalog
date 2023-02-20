using MyPhotosCatalog.Models;

namespace MyPhotosCatalog.Repositories
{
    public interface IRepository
    {
        void AddComment(Comment comment);

        void DeletePhoto(Photo photo);

        Photo GetPhoto(int id);

        IEnumerable<Photo> GetPhotos();

        IEnumerable<Category> GetCategories();

        IEnumerable<Photo> GetMostRatedPhotos();


        void InsertPhoto(Photo photo);

        void UpdatePhoto(Photo photo);

        void DeleteComment(int id);
        Photo FindPhotoByCommentId(int commentId);
    }
}
