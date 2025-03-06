namespace WebServiceProject.Models
{
    [Serializable]
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Author { get; set; }

        //public int? BorrowerId { get; set; }
        //public User Borrower { get; set; }

        public ICollection<Genre> Genres { get; set; }

        // Relation avec les critiques du film
        public ICollection<Review> Reviews { get; set; }

        public ICollection<User> Users { get; set; }

        public Book()
        {
            Title = string.Empty;
            Text = string.Empty;
            Author = string.Empty;
        }

        // Calcul de la note moyenne à partir des critiques
        public double GetAverageRating()
        {
            if (Reviews == null || !Reviews.Any())
            {
                return 0;  // Pas de critiques, pas de note
            }
            return Reviews.Average(r => r.Rating);
        }
    }
}
