using System.Text.Json;
using DataAccessLayer.Model;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repository;

public class FileAuthorRepository : IAuthorRepository
{

    private readonly string authorFilePath;

    public FileAuthorRepository(IConfiguration configuration)
    {
        string projectRoot = Directory.GetCurrentDirectory();
        authorFilePath = Path.Combine(projectRoot, "FileDatabase", "Author.json");
    }

    public IEnumerable<Author>? GetAllAuthors()
    {
        var authors = new List<Author>();

        try
        {
            string jsonString = File.ReadAllText(authorFilePath);
            authors = JsonSerializer.Deserialize<List<Author>>(jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Yazarlar aranırken bir hata oluştu: " + ex.Message);
        }

        return authors;
    }

    public Author GetAuthorById(int id)
    {
        try
        {
            string jsonString = File.ReadAllText(authorFilePath);
            List<Author> authors = JsonSerializer.Deserialize<List<Author>>(jsonString);
            Author author = authors.FirstOrDefault(a => a.Id == id);
            return author;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Yazar aranırken bir hata oluştu: " + ex.Message);
            return null;
        }
    }

    public void AddAuthor(Author newAuthor)
    {
        try
        {
            string jsonString = File.ReadAllText(authorFilePath);

            List<Author> authors = JsonSerializer.Deserialize<List<Author>>(jsonString);

            authors.Add(newAuthor);

            string newJsonString = JsonSerializer.Serialize(authors, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(authorFilePath, newJsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Yazar eklenirken bir hata oluştu: " + ex.Message);
        }
    }

    public void UpdateAuthor(Author updatedAuthor)
    {
        try
        {
            string jsonString = File.ReadAllText(authorFilePath);

            List<Author> authors = JsonSerializer.Deserialize<List<Author>>(jsonString);

            Author existingAuthor = authors.FirstOrDefault(a => a.Id == updatedAuthor.Id);
            if (existingAuthor != null)
            {
                existingAuthor.FirstName = updatedAuthor.FirstName;
                existingAuthor.LastName = updatedAuthor.LastName;
                existingAuthor.BookId = updatedAuthor.BookId;
            }

            string newJsonString = JsonSerializer.Serialize(authors, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(authorFilePath, newJsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Yazar güncellenirken bir hata oluştu: " + ex.Message);
        }
    }

    public void DeleteAuthor(int id)
    {
        try
        {
            string jsonString = File.ReadAllText(authorFilePath);

            List<Author> authors = JsonSerializer.Deserialize<List<Author>>(jsonString);

            Author authorToRemove = authors.FirstOrDefault(a => a.Id == id);
            if (authorToRemove != null)
            {
                authors.Remove(authorToRemove);
            }

            string newJsonString = JsonSerializer.Serialize(authors, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(authorFilePath, newJsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Yazar silinirken bir hata oluştu: " + ex.Message);
        }
    }
}