using Microsoft.EntityFrameworkCore;

using MscApi.Domain.Entities;
using MscApi.DataAccess.Repository.Interfaces;

namespace MscApi.DataAccess.Repository;

public class ProductRepository : IProductRepository
{
    private readonly MedStaCruzContext _context;
    public ProductRepository(MedStaCruzContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        if (_context.Products == null)
            throw new DbUpdateException();

        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetProductById(uint id)
    {
        if (_context.Products == null)
            throw new DbUpdateException();

        var product = await _context.Products.FindAsync(id);

        if(product == null)
         throw new DbUpdateException();

        return product;
    }


    //filtros 
    public async Task<IEnumerable<Product>> GetFilteredProducts(string category = null, decimal? minPrice = null, decimal? maxPrice = null, string brand = null)
    {
        IQueryable<Product> query = _context.Products;

        // Aplicar filtros si se proporcionan valores
        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(p => p.Category == category);
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice);
        }

        if (!string.IsNullOrEmpty(brand))
        {
            query = query.Where(p => p.Brand == brand);
        }

        return await query.ToListAsync();
    }


    public async Task Edit(Product product)
    {
        if (_context.Products == null)
            throw new DbUpdateException();

        try
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

    }

    public async Task Add(Product product)
    {
        if (_context.Products == null)
            throw new DbUpdateException();

        _context.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(uint id)
    {
        if (_context.Products == null)
            throw new DbUpdateException();

        Product? product = await _context.Products.FindAsync(id);

        if(product != null)
            _context.Remove(product);

        await _context.SaveChangesAsync();
    }
}