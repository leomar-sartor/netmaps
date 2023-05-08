using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NetMaps.Interfaces;

namespace NetMaps.Repository
{
    public class GasStationRepository : IGasStationRepository
    {
        private readonly GeoContext _context;

        public GasStationRepository(IOptions<SettingsConfig> settings)
        {
            _context = new GeoContext(settings);
        }

        public async Task AddAsync(GasStation obj)
        {
            await _context.GasStations
                .InsertOneAsync(obj);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.GasStations
                .DeleteOneAsync(doc => doc.Id == id);
        }

        public async Task<IEnumerable<GasStation>> GetAllAsync()
        {
            return await _context.GasStations
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<GasStation> GetByIdAsync(Guid id)
        {
            return await _context.GasStations
                .AsQueryable()
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        //nesse método, eu passo como parâmetro a latitude e longitude, do ponto que estou no GPS. Ele utilizará como filtro.
        public async Task<IEnumerable<GasStationLocationValueObject>> GetProximityAsync(double latitude, double longitude, string name)
        {
            var filter = new BsonDocument { { "name", new BsonRegularExpression($"/^{name}/") } };
            var pipeline = new BsonDocumentPipelineStageDefinition<GasStation, GasStationLocationValueObject>(new BsonDocument
            {
                { "$geoNear", new BsonDocument
                    {
                        { "near", new BsonDocument
                            {
                                { "type", "Point"},
                                { "coordinates", new BsonArray(new[] { latitude, longitude })}
                            }
                        },
                        //distanceField: Informo ao MongoDb, que quero que retorne a distância, na propriedade Dist.Calculated da minha classe GasStationLocationValueObject.cs
                        { "distanceField", "dist.calculated" },
                        //maxDistance: Informo ao MongoDb, que quero que retorne todos os postos de gasolina em um raio de no máximo de 10 quilômetros.
                        { "maxDistance", 10000 },
                        //query: Adiciono o filtro que quero fazer a consulta.
                        { "query" , filter },
                        //includeLocs: Informo ao MongoDb, que quero que retorne a latitude e longitude do posto de gasolina em questão, na propriedade Dist.Location da minha classe GasStationLocationValueObject.cs
                        { "includeLocs", "dist.location" },
                        //spherical: Informo ao MongoDb, que quero que a consulta em quilômetros seja em forma de raio, e não de forma plana.
                        { "spherical", true},
                    }
                }
            });

            var colAggregate = _context.GasStations.Aggregate().AppendStage(pipeline).Limit(10);
            return await colAggregate.ToListAsync();
        }

        public async Task UpdateAsync(GasStation obj)
        {
            var filter = Builders<GasStation>.Filter.Eq(x => x.Id, obj.Id);

            var updateDefinition = Builders<GasStation>.Update
                .Set(x => x.Email, obj.Email)
                .Set(x => x.Name, obj.Name)
                .Set(x => x.Location, obj.Location);

            await _context.GasStations.UpdateOneAsync(filter, updateDefinition);
        }
    }
}
