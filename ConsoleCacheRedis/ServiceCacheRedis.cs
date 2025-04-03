using StackExchange.Redis;

namespace ConsoleCacheRedis
{
    public class ServiceCacheRedis
    {
        private IDatabase database;

        public ServiceCacheRedis() {
            this.database = HelperCacheMultiplexer.Connection.GetDatabase();
        }

        public async Task AddProductoFavoritoAsync(Producto producto) {
            string jsonProductos = await this.database.StringGetAsync("favoritos");

            List<Producto> productos;
            if (jsonProductos != null) {
                productos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
            }else {
                productos = new List<Producto>();
            }

            productos.Add(producto);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(productos);
            await this.database.StringSetAsync("favoritos", json);
        }

        public async Task<List<Producto>> GetProductosFavoritosAsync() {
            string json = await this.database.StringGetAsync("favoritos");
            if (json != null) {
                List<Producto> productos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Producto>>(json);
                return productos;
            }
            return new List<Producto>();
        }

        public async Task RemoveProductoFavoritoAsync(int id) {
            string json = await this.database.StringGetAsync("favoritos");
            if (json != null) {
                List<Producto> productos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Producto>>(json);
                Producto producto = productos.FirstOrDefault(p => p.IdProducto == id);
                if (producto != null) {
                    productos.Remove(producto);
                    if (productos.Count == 0) {
                        await this.database.KeyDeleteAsync("favoritos");
                    } else {
                        string jsonProductos = Newtonsoft.Json.JsonConvert.SerializeObject(productos);
                        await this.database.StringSetAsync("favoritos", jsonProductos, TimeSpan.FromMinutes(30));
                    }
                }
            }
        }
    }
}
