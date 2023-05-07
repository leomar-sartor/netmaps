using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace NetMaps
{
    //https://medium.com/xp-inc/mongodb-utilizando-dados-geogr%C3%A1ficos-b0e07a31211d
    public class GeoContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GeoContext(IOptions<SettingsConfig> settings)
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            var client = new MongoClient(settings.Value.ConnectionString);
            _mongoDatabase = client.GetDatabase(settings.Value.Database);

            CheckIndexContext();
        }

        public IMongoCollection<Pontos> Pontos =>
            _mongoDatabase.GetCollection<Pontos>("pontos");

        private void CheckIndexContext()
        {
            IMongoCollection<Pontos> collection = _mongoDatabase.GetCollection<Pontos>("pontos");
            collection.Indexes.CreateOne(new IndexKeysDefinitionBuilder<Pontos>().Geo2DSphere(x => x.Name));
        }
    }
}
