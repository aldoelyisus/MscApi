using System.Threading.Tasks;
using MscApi.Domain.Entities;

namespace MscApi.DataAccess.Repository.Interfaces;

public interface ILocationRepository
{
    Task<IEnumerable<StoreLocation>> GetLocations();
}