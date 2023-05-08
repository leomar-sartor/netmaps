using MongoDB.Driver;
using NetMaps.Models;
using System.Collections;

namespace NetMaps
{
    public class ContextoMongoDb
    {
        public static string ConnectionString { get; set; }

        public static string DatabaseName { get; set; }

        public static bool IsSSL { get; set; }

        private IMongoDatabase _database { get; }


        public ContextoMongoDb()
        {
            try
            {
                MongoClientSettings setting = MongoClientSettings
                    .FromUrl(new MongoUrl(ConnectionString));

                if (IsSSL)
                {
                    setting.SslSettings = new SslSettings
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    };
                }

                var mongoCliente = new MongoClient(setting);
                _database = mongoCliente.GetDatabase(DatabaseName);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível conectar!");
            }
        }

        public void CheckIndexContext()
        {
            IMongoCollection<GasStation> collection = _database.GetCollection<GasStation>("GasStations");

            //var options = new CreateIndexOptions
            //{
            //    Unique = isUnique,
            //    Name = $"{collectionName}_{fieldName}"
            //};

            //var createdIndexName = collection.Indexes.CreateOne($"{{ {fieldName} : 1 }}", options);

            collection.Indexes.CreateOne(
                new IndexKeysDefinitionBuilder<GasStation>().Geo2DSphere(x => x.Location)
            );
        }

        public IMongoCollection<Usuario> Usuario
        {
            get
            {
                return _database.GetCollection<Usuario>("Usuario");
            }
        }

        
    }
}
