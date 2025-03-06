using WebServiceProject.Data;
using WebServiceProject.Models;

namespace WebServiceProject
{
    public class Seed(DataContext context)
    {
        private readonly DataContext _context = context;

        public void SeedDataContext()
        {
            if (!_context.Books.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Pseudo = "KHALIL",
                        Password = "yourpassword",
                        UserRole = Role.Admin,
                        Reviews = new List<Review>
                        {
                            new Review
                            {
                                Title = "Fantastic Read!",
                                Text = "This book was incredibly inspiring and captivating.",
                                Rating = 5,
                                Book = new Book
                                {
                                    Title = "1984",
                                    Author = "George Orwell",
                                    ReleaseDate = new DateTime(1949, 6, 8)
                                }
                            },
                            new Review
                            {
                                Title = "Thought-provoking",
                                Text = "A compelling story with deep themes.",
                                Rating = 4,
                                Book = new Book
                                {
                                    Title = "Brave New World",
                                    Author = "Aldous Huxley",
                                    ReleaseDate = new DateTime(1932, 8, 31)
                                }
                            }
                        }
                    }
                };

                var books = new List<Book>()
                {
                    new Book()
                    {
                        Title = "1984",
                        Author = "George Orwell",
                        ReleaseDate = new DateTime(1949, 6, 8),
                        Genres = new List<Genre>()
                        {
                            new Genre { Name = "Dystopian" },
                            new Genre { Name = "Political Fiction" }
                        },
                        Reviews = new List<Review>()
                        {
                            new() { Title = "Amazing", Text = "A timeless masterpiece.", Rating = 5, Reviewer = new User() { Pseudo = "Reader1", Password = "password123" } },
                            new() { Title = "Great Read", Text = "Profound and impactful.", Rating = 4, Reviewer = new User() { Pseudo = "Reader2", Password = "password456" } }
                        }
                    },
                    new Book()
                    {
                        Title = "To Kill a Mockingbird",
                        Author = "Harper Lee",
                        ReleaseDate = new DateTime(1960, 7, 11),
                        Genres = new List<Genre>()
                        {
                            new Genre { Name = "Historical Fiction" },
                            new Genre { Name = "Social Issues" }
                        },
                        Reviews = new List<Review>()
                        {
                            new() { Title = "Heartfelt", Text = "A powerful tale of justice.", Rating = 5, Reviewer = new User() { Pseudo = "AtticusFan", Password = "justice123" } },
                            new() { Title = "Emotional", Text = "Truly moving and well-written.", Rating = 5, Reviewer = new User() { Pseudo = "Scout", Password = "mockingbird456" } }
                        }
                    },
                    new Book()
                    {
                        Title = "Pride and Prejudice",
                        Author = "Jane Austen",
                        ReleaseDate = new DateTime(1813, 1, 28),
                        Genres = new List<Genre>()
                        {
                            new Genre { Name = "Romance" },
                            new Genre { Name = "Classic" }
                        },
                        Reviews = new List<Review>()
                        {
                            new() { Title = "Charming", Text = "A delightful read.", Rating = 5, Reviewer = new User() { Pseudo = "ElizabethFan", Password = "darcy123" } },
                            new() { Title = "Classic", Text = "Timeless and engaging.", Rating = 4, Reviewer = new User() { Pseudo = "MrDarcy", Password = "romance456" } }
                        }
                    }
                };

                _context.Books.AddRange(books);
                _context.SaveChanges();
            }
        }
    }
}
