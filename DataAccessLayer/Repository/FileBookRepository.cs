using System.Text.Json;
using DataAccessLayer.Model;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repository;

public class FileBookRepository : IBookRepository
{

    private readonly string bookFilePath;

    public FileBookRepository(IConfiguration configuration)
    {
        string projectRoot = Directory.GetCurrentDirectory();
        bookFilePath = Path.Combine(projectRoot, "FileDatabase", "Book.json");
    }

    public IEnumerable<Book>? GetAllBooks()
    {
        var books = new List<Book>();

        try
        {
            string jsonString = File.ReadAllText(bookFilePath);
            books = JsonSerializer.Deserialize<List<Book>>(jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Kitaplar aranırken bir hata oluştu: " + ex.Message);
        }

        return books;
    }

    public Book GetBookById(int id)
    {
        try
        {
            string jsonString = File.ReadAllText(bookFilePath);
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(jsonString);
            Book book = books.FirstOrDefault(b => b.Id == id);
            return book;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Kitap aranırken bir hata oluştu: " + ex.Message);
            return null;
        }
    }

    public void AddBook(Book newBook)
    {
        try
        {
            string jsonString = File.ReadAllText(bookFilePath);

            List<Book> books = JsonSerializer.Deserialize<List<Book>>(jsonString);

            books.Add(newBook);

            string newJsonString = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(bookFilePath, newJsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Kitap eklenirken bir hata oluştu: " + ex.Message);
        }
    }

    public void UpdateBook(Book updatedBook)
    {
        try
        {
            string jsonString = File.ReadAllText(bookFilePath);

            List<Book> books = JsonSerializer.Deserialize<List<Book>>(jsonString);

            Book existingBook = books.FirstOrDefault(b => b.Id == updatedBook.Id);
            if (existingBook != null)
            {
                existingBook.Title = updatedBook.Title;
                existingBook.Price = updatedBook.Price;
                existingBook.ISBN = updatedBook.ISBN;
            }

            string newJsonString = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(bookFilePath, newJsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Kitap güncellenirken bir hata oluştu: " + ex.Message);
        }
    }

    public void DeleteBook(int id)
    {
        try
        {
            string jsonString = File.ReadAllText(bookFilePath);

            List<Book> books = JsonSerializer.Deserialize<List<Book>>(jsonString);

            Book bookToRemove = books.FirstOrDefault(b => b.Id == id);
            if (bookToRemove != null)
            {
                books.Remove(bookToRemove);
            }

            string newJsonString = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(bookFilePath, newJsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Kitap silinirken bir hata oluştu: " + ex.Message);
        }
    }
}