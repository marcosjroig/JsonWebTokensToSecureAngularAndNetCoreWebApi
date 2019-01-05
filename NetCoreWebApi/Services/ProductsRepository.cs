using System.Collections.Generic;
using System.Linq;
using PtcApi.Data;
using PtcApi.Model;

namespace PtcApi.Services
{
    public class ProductsRepository: IProducts
    {
        private readonly PtcDbContext _ptcDbContext;
        public ProductsRepository(PtcDbContext ptcDbContext)
        {
            _ptcDbContext = ptcDbContext;
        }

        public IEnumerable<Product> GetAll()
        {
            return _ptcDbContext.Products;
        }

        public Product GetById(int id)
        {
            var product = _ptcDbContext.Products.SingleOrDefault(x => x.ProductId == id);
            return product;
        }

        public void AddProduct(Product product)
        {
            _ptcDbContext.Products.Add(product);
            _ptcDbContext.SaveChanges(true);
        }

        public void UpdateProduct(Product product)
        {
            _ptcDbContext.Products.Update(product);
            _ptcDbContext.SaveChanges(true);
        }

        public void Delete(int id)
        {
            //1) Get the object
            var product = _ptcDbContext.Products.Find(id);

            //2) Delete the object
            _ptcDbContext.Products.Remove(product);
            _ptcDbContext.SaveChanges(true);
        }
    }
}
