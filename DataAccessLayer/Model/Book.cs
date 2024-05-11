namespace DataAccessLayer.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ISBN { get; set; }
        public int PublisherId { get; set; }
    }
}
