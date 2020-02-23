using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Andreys.Data;
using Andreys.Models;
using Andreys.Models.Enums;
using Andreys.ViewModels.Products;

namespace Andreys.Services
{
    class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }
        public int Add(string name, string description, string imageUrl, string category,string gender, decimal price)
        {
            var product = new Product()
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                Category = Enum.Parse<Category>(category),
                Gender = Enum.Parse<Gender>(gender),
                Price = price
            };

            this.db.Products.Add(product);
            db.SaveChanges();

            return product.Id;
        }

        public IEnumerable<Product> GetAll()
        {
            var products = this.db.Products.Select(x => new Product()
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price
            }).ToArray();

            return products;
        }

        public Product GetById(int id)
        {
            var product = this.db.Products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        public void DeleteById(int id)
        {
            var productToRemove = this.db.Products.FirstOrDefault(p => p.Id == id);

            db.Products.Remove(productToRemove);
            db.SaveChanges();
        }
    }
}
