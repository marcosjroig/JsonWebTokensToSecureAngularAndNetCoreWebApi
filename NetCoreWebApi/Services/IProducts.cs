using System.Collections.Generic;
using PtcApi.Model;

namespace PtcApi.Services
{
    public interface IProducts
    {
        // CRUD operations
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void Delete(int id);
    }
}
