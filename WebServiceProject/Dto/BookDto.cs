namespace WebServiceProject.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public BookDto() { Title = string.Empty; }
    }
}
