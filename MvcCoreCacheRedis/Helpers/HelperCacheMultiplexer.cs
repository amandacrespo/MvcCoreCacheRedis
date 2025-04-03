using StackExchange.Redis;

namespace MvcCoreCacheRedis.Helpers
{
    public static class HelperCacheMultiplexer
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string cacheRedis = configuration["AzureKeys:CacheRedis"];
            return ConnectionMultiplexer.Connect(cacheRedis);
        });

        public static ConnectionMultiplexer Connection {
            get {
                return lazyConnection.Value;
            }
        }

        public static IDatabase Database => Connection.GetDatabase();
        public static IServer Server => Connection.GetServer("localhost", 6379);
        public static void Close() {
            if (lazyConnection.IsValueCreated) {
                lazyConnection.Value.Close();
            }
        }
    }
}
