using NetMaps.Interfaces;
using NetMaps.Models;

namespace NetMaps.Services
{
    public class GasStationAppService : IGasStationAppService
    {
        private readonly IGasStationRepository _gasStationRepository;

        public GasStationAppService(IGasStationRepository gasStationRepository)
        {
            _gasStationRepository = gasStationRepository;
        }
        public async Task<GasStation> AddAsync(GasStationInput input)
        {
            var obj = new GasStation()
            {
                Id = Guid.NewGuid(),
                Email = input.Email,
                Name = input.Name,
                Location = input.Location
            };

            await _gasStationRepository.AddAsync(obj);

            return obj;
        }

        public async Task<GasStation> GetByIdAsync(Guid id)
        {
            return await _gasStationRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<GasStation>> GetAllAsync()
        {
            return await _gasStationRepository.GetAllAsync();
        }

        public async Task<IEnumerable<GasStationLocationValueObject>> GetProximityAsync(double latitude, double longitude, string name)
        {
            return await _gasStationRepository.GetProximityAsync(latitude, longitude, name);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _gasStationRepository.DeleteAsync(id);
        }

        public async Task<GasStation> UpdateAsync(Guid id, GasStationInput input)
        {
            var obj = new GasStation()
            {
                Id = id,
                Email = input.Email,
                Name = input.Name,
                Location = input.Location
            };

            await _gasStationRepository.UpdateAsync(obj);
            return await _gasStationRepository.GetByIdAsync(obj.Id);
        }
    }
}
