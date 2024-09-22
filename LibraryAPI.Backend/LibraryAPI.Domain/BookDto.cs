namespace LibraryAPI.Domain
{
    public class BookDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } // Убедитесь, что это поле есть
        public DateTime? BorrowedAt { get; set; }
        public DateTime? ReturnBy { get; set; }
        public string ImageUrl { get; set; }
        public bool IsBorrowed { get; set; }
    }

}
