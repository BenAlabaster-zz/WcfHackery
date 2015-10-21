using System;
using System.Collections.Generic;
using System.Security.Permissions;
using WcfRestAuthentication.Model;
using WcfRestAuthentication.Services.Api.Endpoints.Product;

namespace WcfRestAuthentication.Services.Api
{
    public partial class ApiService : IProductService
    {
        public IEnumerable<Product> GetList(Guid categoryId, int pageIndex, int pageSize)
        {
            var category1 = Guid.NewGuid();
            var category2 = Guid.NewGuid();

            return new List<Product>
            {
                Product.Create("Product1", "First Product", category1),
                Product.Create("Product2", "Second Product", category1),
                Product.Create("Product3", "Third Product", category2)
            };
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Contributor")]
        public Product Put(Product product)
        {
            return ProductRepository.Update(product);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        Product IProductService.Get(Guid productId)
        {
            return ProductRepository.Get(productId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Contributor")]
        public Product Post(Product product)
        {
            return ProductRepository.Update(product);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        public void DeleteProduct(Guid productId)
        {
            ProductRepository.Delete(productId);
        }
    }
}