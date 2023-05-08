using NetMaps.Models;

namespace NetMaps.Interfaces
{
    public interface IGasStationAppService
    {
        Task<GasStation> AddAsync(GasStationInput obj);
        Task<GasStation> GetByIdAsync(Guid id);
        Task<IEnumerable<GasStation>> GetAllAsync();
        Task<IEnumerable<GasStationLocationValueObject>> GetProximityAsync(double latitude, double longitude, string name);
        Task DeleteAsync(Guid id);
        Task<GasStation> UpdateAsync(Guid Id, GasStationInput obj);
    }
}
