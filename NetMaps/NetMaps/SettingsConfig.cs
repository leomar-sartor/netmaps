namespace NetMaps
{
    public class SettingsConfig
    {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public string Database { get; set; } = "geolocalizacao";
    }
}
