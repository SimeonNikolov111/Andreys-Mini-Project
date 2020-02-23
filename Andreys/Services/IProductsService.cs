using System;
using System.Collections.Generic;
using System.Text;
using Andreys.Models;
using Andreys.ViewModels.Products;

namespace Andreys.Services
{
    public interface IProductsService
    {
        int Add(string name, string description, string imageUrl,string category,string gender, decimal price);

        IEnumerable<Product> GetAll();

        Product GetById(int id);

        void DeleteById(int id);
    }
}
