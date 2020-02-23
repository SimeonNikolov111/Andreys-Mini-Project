using System;
using System.Collections.Generic;
using System.Text;
using Andreys.Services;
using Andreys.ViewModels.Products;
using SIS.HTTP;
using SIS.MvcFramework;

namespace Andreys.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productService;

        public ProductsController(IProductsService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public HttpResponse Add()
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("Users/Login");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddProductInputModel inputModel)
        {
            if (inputModel.Name.Length < 4 || inputModel.Name.Length > 20 || inputModel.Description.Length > 10)
            {
                return this.Redirect("/Products/Add");
            }

            int productId = productService.Add(inputModel.Name,inputModel.Description,inputModel.ImageUrl,inputModel.Category,inputModel.Gender,inputModel.Price);
            return this.Redirect($"/Products/Details?id={productId}");
        }

        [HttpGet]
        public HttpResponse Details(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var product = this.productService.GetById(id);

            return this.View(product);
        }

        [HttpGet]
        public HttpResponse Delete(int id)
        {
            this.productService.DeleteById(id);
            return this.Redirect("/");
        }
    }
}
