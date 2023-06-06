using Microsoft.EntityFrameworkCore;

using MscApi.Domain.Entities;
using MscApi.DataAccess.Repository.Interfaces;

namespace MscApi.DataAccess.Repository;

public class LocationRepository : ILocationRepository
{
    private readonly MedStaCruzContext _context;
    public LocationRepository(MedStaCruzContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StoreLocation>> GetLocations()
    {
        IEnumerable<StoreLocation> locations;

        if(_context.StoreLocations == null)
            throw new DbUpdateException();

        locations = await _context.StoreLocations.ToListAsync();

        return locations;
    }
}
