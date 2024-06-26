﻿using DataAccessLayer.Model;

namespace DataAccessLayer.Repository;

public interface IPublisherRepository
{
    IEnumerable<Publisher>? GetAllPublishers();
    Publisher GetPublisherById(int id);
    void AddPublisher(Publisher publisher);
    void UpdatePublisher(Publisher publisher);
    void DeletePublisher(int id);
}