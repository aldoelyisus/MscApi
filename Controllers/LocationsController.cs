using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using MscApi.DataAccess.Repository.Interfaces;
using MscApi.Domain.Entities;

namespace MscApi.Controllers;

[Route("api/stores")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ILogger<LocationController> _logger;
    private readonly IMapper _mapper;
    private readonly ILocationRepository _repository;

    public LocationController(ILocationRepository repository, ILogger<LocationController> logger, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<StoreLocation>>> GetLocations()
    {
        IEnumerable<StoreLocation> locations;

        try
        {
            locations = await _repository.GetLocations();

            if(locations == null)
                return NotFound();
        }
        catch(Exception)
        {
            return NoContent();
        }

        return Ok(locations);
    }
}