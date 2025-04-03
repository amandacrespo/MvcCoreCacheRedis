using ConsoleCacheRedis;

Console.WriteLine("Testing cahce redis");
Console.WriteLine("Pulse para iniciar ->");
Console.ReadLine();

ServiceCacheRedis service = new ServiceCacheRedis();
List<Producto> favoritos = await service.GetProductosFavoritosAsync();
if (favoritos == null) {
    Console.WriteLine("No hay productos favoritos");
}
else {
    Console.WriteLine("Productos favoritos:");
    int i = 1;
    foreach (var item in favoritos) {
        Console.WriteLine($"Id: {item.IdProducto}, Nombre: {item.Nombre}, Precio: {item.Precio}");
        i++;
    }
}

