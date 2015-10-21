using System;
using WcfRestAuthentication.Model;

namespace WcfRestAuthentication.Data
{
    public interface IProductRepository
    {
        Product Get(Guid productId);
        Product Update(Product product);
        void Delete(Guid productId);
    }
}