using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using MscApi.AutoMapperProfiles;
using MscApi.DataAccess.DTO;
using MscApi.DataAccess;
using MscApi.DataAccess.Repository.Interfaces;
using MscApi.Domain.Entities;

namespace MscApi.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IMapper _mapper;
    private readonly IProductRepository _repository;


    public ProductsController(IProductRepository repository, ILogger<ProductsController> logger, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
        IEnumerable<Product> products;
        IEnumerable<ProductDTO> productsDTO;

        products = await _repository.GetProducts();

        if(products == null)
            return NotFound();
        
        productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

        return Ok(productsDTO);
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProductsById(uint id)
    {
        Product product;
        ProductDTO productDTO;

        try
        {
            product = await _repository.GetProductById(id);

            if (product == null)
                return NotFound();

            productDTO = _mapper.Map<ProductDTO>(product);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            _logger.LogInformation(e.StackTrace);
            return NoContent();
        }

        return Ok(productDTO);
    }

    //    // PUT: api/Products/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutProduct(uint id, Product product)
    //    {
    //        if (id != product.Id)
    //            return BadRequest();
    //
    //        _context.Entry(product).State = EntityState.Modified;
    //
    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!ProductExists(id))
    //                return NotFound();
    //            else
    //                throw;
    //        }
    //
    //        return NoContent();
    //    }
    //
    //    // POST: api/Products
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPost]
    //    public async Task<ActionResult<Product>> PostProduct(Product product)
    //    {
    //        if (_context.Products == null)
    //            return Problem("Entity set 'MedStaCruzContext.Products'    is null.");
    //
    //        _context.Products.Add(product);
    //        await _context.SaveChangesAsync();
    //
    //        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    //    }
    //
    //    // DELETE: api/Products/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteProduct(uint id)
    //    {
    //        if (_context.Products == null)
    //            return NotFound();
    //
    //        var product = await _context.Products.FindAsync(id);
    //
    //        if (product == null)
    //            return NotFound();
    //
    //        _context.Products.Remove(product);
    //        await _context.SaveChangesAsync();
    //
    //        return NoContent();
    //    }
    //
    //    private bool ProductExists(uint id)
    //    {
    //        return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
    //    }




    // GET: api/products/filter
    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetFilteredProducts(string category = null, decimal? minPrice = null, decimal? maxPrice = null, string brand = null)
    {
        IEnumerable<Product> products;
        IEnumerable<ProductDTO> productsDTO;

        products = await _repository.GetProducts();

        if (products == null)
            return NotFound();

        // Aplicar filtros si se proporcionan valores
        if (!string.IsNullOrEmpty(category))
        {
            products = products.Where(p => p.Category == category);
        }

        if (minPrice.HasValue)
        {
            products = products.Where(p => p.Price >= minPrice);
        }

        if (maxPrice.HasValue)
        {
            products = products.Where(p => p.Price <= maxPrice);
        }

        if (!string.IsNullOrEmpty(brand))
        {
            products = products.Where(p => p.Brand == brand);
        }

        productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

        return Ok(productsDTO);
    }
}