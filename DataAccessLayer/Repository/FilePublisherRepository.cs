using System.Text.Json;
using DataAccessLayer.Model;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repository;

public class FilePublisherRepository : IPublisherRepository
{

    private readonly string publisherFilePath;

    public FilePublisherRepository(IConfiguration configuration)
    {
        string projectRoot = Directory.GetCurrentDirectory();
        publisherFilePath = Path.Combine(projectRoot, "FileDatabase", "Publisher.json");
    }

    public IEnumerable<Publisher>? GetAllPublishers()
    {
        var publishers = new List<Publisher>();

        try
        {
            string jsonString = File.ReadAllText(publisherFilePath);
            publishers = JsonSerializer.Deserialize<List<Publisher>>(jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Yayınevleri okunurken bir hata oluştu: " + ex.Message);
        }

        return publishers;
    }

    public Publisher GetPublisherById(int id)
    {
        try
        {
            string jsonString = File.ReadAllText(publisherFilePath);
            List<Publisher> publishers = JsonSerializer.Deserialize<List<Publisher>>(jsonString);
            Publisher publisher = publishers.FirstOrDefault(p => p.Id == id);
            return publisher;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Yayınevi okunurken bir hata oluştu: " + ex.Message);
            return null;
        }
    }

    public void AddPublisher(Publisher newPublisher)
    {
        try
        {
            string jsonString = File.ReadAllText(publisherFilePath);

            List<Publisher> publishers = JsonSerializer.Deserialize<List<Publisher>>(jsonString);

            publishers.Add(newPublisher);

            string newJsonString = JsonSerializer.Serialize(publishers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(publisherFilePath, newJsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Yayınevi eklenirken bir hata oluştu: " + ex.Message);
        }
    }

    public void UpdatePublisher(Publisher updatedPublisher)
    {
        try
        {
            string jsonString = File.ReadAllText(publisherFilePath);

            List<Publisher> publishers = JsonSerializer.Deserialize<List<Publisher>>(jsonString);

            Publisher existingPublisher = publishers.FirstOrDefault(p => p.Id == updatedPublisher.Id);
            if (existingPublisher != null)
            {
                existingPublisher.Name = updatedPublisher.Name;
            }

            string newJsonString = JsonSerializer.Serialize(publishers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(publisherFilePath, newJsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Yayınevi güncellenirken bir hata oluştu: " + ex.Message);
        }
    }

    public void DeletePublisher(int id)
    {
        try
        {
            string jsonString = File.ReadAllText(publisherFilePath);

            List<Publisher> publishers = JsonSerializer.Deserialize<List<Publisher>>(jsonString);

            Publisher publisherToRemove = publishers.FirstOrDefault(p => p.Id == id);
            if (publisherToRemove != null)
            {
                publishers.Remove(publisherToRemove);
            }

            string newJsonString = JsonSerializer.Serialize(publishers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(publisherFilePath, newJsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Yayınevi silinirken bir hata oluştu: " + ex.Message);
        }
    }
}