namespace NetMaps
{
    //https://medium.com/xp-inc/mongodb-utilizando-dados-geogr%C3%A1ficos-b0e07a31211d
    public class GeoContext
    {
        //private readonly IMongoDatabase _mongoDatabase;

        //public GeoContext(IOptions<SettingsConfig> settings)
        //{
        //    var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        //    ConventionRegistry.Register("camelCase", conventionPack, t => true);

        //    var client = new MongoClient(settings.Value.ConnectionString);
        //    _mongoDatabase = client.GetDatabase(settings.Value.Database);

        //    CheckIndexContext();
        //}

        //public IMongoCollection<GasStation> GasStations =>
        //    _mongoDatabase.GetCollection<GasStation>("GasStations");

        //private void CheckIndexContext()
        //{
        //    IMongoCollection<GasStation> collection = _mongoDatabase.GetCollection<GasStation>("GasStations");
        //    collection.Indexes.CreateOne(new IndexKeysDefinitionBuilder<GasStation>().Geo2DSphere(x => x.Location));
        //}
    }
}
