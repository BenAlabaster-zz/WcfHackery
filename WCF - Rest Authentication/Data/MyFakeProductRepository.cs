using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WcfRestAuthentication.Model;

namespace WcfRestAuthentication.Data
{
    public class MyFakeProductRepository : IProductRepository
    {
        public Model.Product Get(System.Guid productId)
        {
            return Product.Create("Ben's Beans", "Can 250g Ben's Beans", Guid.NewGuid());
        }

        public IQueryable<Model.Product> Query(Expression<Func<Product, bool>> queryExpression)
        {
            Guid category1 = Guid.NewGuid();
            Guid category2 = Guid.NewGuid();

            return new List<Model.Product>()
            {
                Product.Create("Ben's Beans", "Can 250g Ben's Beans", category1),
                Product.Create("Ben's Spaghetti", "Can 250g Ben's Spaghetti", category1),
                Product.Create("Ben's Bullets", "5000 7.62mm Teflon Tipped Bullets", category2)
            }.AsQueryable();
        }

        public Model.Product Update(Model.Product product)
        {
            return product;
        }

        public void Delete(System.Guid productId)
        {
        }
    }
}