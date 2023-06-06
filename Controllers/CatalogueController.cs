using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using MscApi.AutoMapperProfiles;
using MscApi.DataAccess.DTO;
using MscApi.DataAccess.Repository.Interfaces;
using MscApi.Domain.Entities;

namespace MscApi.Controllers;

[Route("api/catalogue")]
[ApiController]
public class CatalogueController : ControllerBase
{
    private readonly ILogger<CatalogueController> _logger;
    private readonly IMapper _mapper;
    private readonly ICatalogueRepository _repository;

    public CatalogueController(ICatalogueRepository repository, ILogger<CatalogueController> logger, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<ProductCardDTO>>> GetProductsByCategory(string category)
    {
        IEnumerable<Product> products;
        IEnumerable<ProductCardDTO> productsDTO;

        _logger.LogInformation("Obteniendo productos por categoría");

        try
        {
            products = await _repository.GetProductsByCategory(category);

            if(products == null)
                return NotFound();

            productsDTO = _mapper.Map<IEnumerable<ProductCardDTO>>(products);
        }
        catch(Exception e)
        {
            _logger.LogTrace(e.StackTrace);
            return NoContent();
        }

        return Ok(productsDTO);
    }
}