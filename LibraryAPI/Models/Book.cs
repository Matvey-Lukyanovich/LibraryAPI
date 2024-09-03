using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public DateTime? BorrowedAt { get; set; }
        public DateTime? ReturnBy { get; set; }
        public string ImageUrl { get; set; }
        public bool IsBorrowed { get; set; }

        // Навигационное свойство для Author
        public Author? Author { get; set; }
    }

}
