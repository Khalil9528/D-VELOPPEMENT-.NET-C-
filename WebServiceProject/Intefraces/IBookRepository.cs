using WebServiceProject.Models;

namespace WebServiceProject.Intefraces
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks();
        Book GetBookById(int id);
        Book GetBookByTitle(string title);
       
    }
}
