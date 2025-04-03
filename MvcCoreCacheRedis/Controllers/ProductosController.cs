using Microsoft.AspNetCore.Mvc;
using MvcCoreCacheRedis.Models;
using MvcCoreCacheRedis.Repositories;
using MvcCoreCacheRedis.Services;

namespace MvcCoreCacheRedis.Controllers
{
    public class ProductosController : Controller
    {
        private RepositoryProductos repo;
        private ServiceCacheRedis serviceCache;

        public ProductosController(RepositoryProductos repo, ServiceCacheRedis serviceCache) {
            this.repo = repo;
            this.serviceCache = serviceCache;
        }

        public IActionResult Index() {
            List<Producto> productos = this.repo.GetProductos();
            return View(productos);
        }

        public IActionResult Details(int id) {
            Producto producto = this.repo.FindProducto(id);
            return View(producto);
        }

        public async Task<IActionResult> Favoritos() {
            List<Producto> productos = await this.serviceCache.GetProductosFavoritosAsync();
            return View(productos);
        }

        public async Task<IActionResult> SeleccionarFavorito(int id) {
            Producto producto = this.repo.FindProducto(id);
            await this.serviceCache.AddProductoFavoritoAsync(producto);
            return RedirectToAction("Favoritos");
        }

        public async Task<IActionResult> EliminarFavorito(int id) {
            await this.serviceCache.RemoveProductoFavoritoAsync(id);
            return RedirectToAction("Favoritos");
        }

    }
}
