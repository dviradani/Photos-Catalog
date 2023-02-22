using Microsoft.EntityFrameworkCore;
using MyPhotosCatalog.Models;

namespace MyPhotosCatalog.DAL
{
    public class PhotosCatalogContext : DbContext
    {
        public PhotosCatalogContext(DbContextOptions<PhotosCatalogContext> options) : base(options) { }

        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            new Category() { Id = 1, Name = "Nature" },
            new Category() { Id = 2, Name = "Vacation" },
            new Category() { Id = 3, Name = "Lifestyle" });

            modelBuilder.Entity<Photo>().HasData(
             new Photo() { Id = 1, Name = "Cool guy plays guitar", CategoryId = 3, PictureName = "BrettSayles.png", Owner = "Brett Sayles" },
             new Photo() { Id = 2, Name = "Welcome to paradise", CategoryId = 2, PictureName = "ChrisEdghill.png", Owner = "Chris Edghill" },
             new Photo() { Id = 3, Name = "On top of the world", CategoryId = 1, PictureName = "ChrisJanda.png", Owner = "Chris Janda" },
             new Photo() { Id = 4, Name = "Beuatiful overiew for the weekend", CategoryId = 1, PictureName = "DavidSanz.png", Owner = "David Sanz" },
             new Photo() { Id = 5, Name = "Scents of spring", CategoryId = 1, PictureName = "JosVanOuwerkerk.png", Owner = "Jos Van Ouwerkerkt" },
             new Photo() { Id = 6, Name = "Surfing just for fun", CategoryId = 3, PictureName = "MikeGlezos.png", Owner = "Mike Glezos" },
             new Photo() { Id = 7, Name = "Old fashion neighborhood", CategoryId = 3, PictureName = "NateCohen.png", Owner = "Nate Cohen" },
             new Photo() { Id = 8, Name = "Perfect for dates", CategoryId = 1, PictureName = "SimonBerger.png", Owner = "Simon Berger" },
             new Photo() { Id = 9, Name = "Memories of a trip to Paris", CategoryId = 2, PictureName = "ИринаЧернышова.png", Owner = "Ирина Чернышова" });

            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = 1, PhotoId = 8, Content = "Really romantic" },
                new Comment { Id = 2, PhotoId = 3, Content = "Scaryyy..." },
                new Comment { Id = 3, PhotoId = 8, Content = "Where is this place?" },
                new Comment { Id = 4, PhotoId = 3, Content = "Woooow :O" },
                new Comment { Id = 5, PhotoId = 1, Content = "Pro photograger" }
                );
        }
    }
}
