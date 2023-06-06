using MscApi.Domain.Entities;

namespace MscApi.DataAccess.Repository.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProductById(uint id);
    Task Add(Product product);
    Task Edit(Product product);
    Task Delete(uint id);
}