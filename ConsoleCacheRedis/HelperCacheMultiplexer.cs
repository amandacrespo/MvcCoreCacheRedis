using StackExchange.Redis;

namespace ConsoleCacheRedis
{
    public class HelperCacheMultiplexer
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheRedis = "cacheredisamanda.redis.cache.windows.net:6380,password=Jn4NXmrr0RSBJYcidtzHc2c0Kbl2YBoN0AzCaBZdva8=,ssl=True,abortConnect=False";
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
