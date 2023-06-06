using System.Collections.Generic;
using System.Threading.Tasks;
using MscApi.Domain.Entities;

namespace MscApi.DataAccess.Repository.Interfaces;

public interface ICatalogueRepository
{
    // public Product GetProductById(uint id);
    Task<IEnumerable<Product>> GetProductsByCategory(string category);
}