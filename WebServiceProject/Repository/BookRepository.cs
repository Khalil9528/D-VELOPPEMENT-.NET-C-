
using WebServiceProject.Data;
using WebServiceProject.Intefraces;
using WebServiceProject.Models;

namespace WebServiceProject.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Book> GetBooks()
        {
            return _context.Books.OrderBy(m => m.Id).ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.Where(m => m.Id == id).FirstOrDefault();
        }

        public Book GetBookByTitle(string title)
        {
            return _context.Books.Where(m => m.Title == title).FirstOrDefault();
        }

        // Méthode pour ajouter un film
        public bool AddBook(Book Book)
        {
            // Ajout du film au contexte
            _context.Books.Add(Book);

            // Sauvegarde des changements
            return Save();
        }

        // Méthode pour sauvegarder les modifications dans la base de données
        public bool Save()
        {
            // Retourne true si une ou plusieurs lignes ont été affectées (changement sauvegardé avec succès)
            return _context.SaveChanges() > 0;
        }
    }
}
