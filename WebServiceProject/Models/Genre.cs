namespace WebServiceProject.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }  // Nom du genre (ex. : Action, Comédie, etc.)
        public ICollection<Book> Books { get; set; }  // Liste des films appartenant à ce genre

        public Genre()
        {
            Name = string.Empty;
        }
    }
}
