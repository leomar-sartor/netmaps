namespace NetMaps.Interfaces
{
    public interface IGasStationRepository
    {
        Task AddAsync(GasStation obj);
        Task<GasStation> GetByIdAsync(Guid id);
        Task<IEnumerable<GasStation>> GetAllAsync();
        Task<IEnumerable<GasStationLocationValueObject>> GetProximityAsync(double latitude, double longitude, string name);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(GasStation id);
    }
}
