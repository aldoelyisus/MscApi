using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using MscApi.DataAccess.Repository.Interfaces;
using MscApi.Domain.Entities;

namespace MscApi.DataAccess.Repository;

public class CatalogueRepository : ICatalogueRepository
{
    private readonly MedStaCruzContext _context;
    public CatalogueRepository(MedStaCruzContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
    {
        if(_context.Products == null)
            throw new DbUpdateException();

        return await _context.Products
            .Where(p => p.Category.Equals(category))
            .ToListAsync();
    }

 //   public Product GetProductById(uint id)
 //   {
 //       if(_context.Products == null)
 //           throw new DbUpdateException();

 //       var product = _context.Products.Find() &&
 //          throw new DbUpdateException();

 //       return product;
 //   }
}