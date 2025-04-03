using MvcCoreCacheRedis.Helpers;
using MvcCoreCacheRedis.Models;
using System.Xml.Linq;

namespace MvcCoreCacheRedis.Repositories
{
    public class RepositoryProductos
    {
        private XDocument doc;

        public RepositoryProductos(HelperPathProvider helper) {
            string path = helper.MapPath(Folders.documents, "productos.xml");
            this.doc = XDocument.Load(path);
        }

        public List<Producto> GetProductos() {
            var productos = from prod in this.doc.Descendants("producto")
                            select new Producto()
                            {
                                IdProducto = (int)prod.Element("idproducto"),
                                Nombre = (string)prod.Element("nombre"),
                                Descripcion = (string)prod.Element("descripcion"),
                                Precio = (int)prod.Element("precio"),
                                Imagen = (string)prod.Element("imagen")
                            };
            return productos.ToList();
        }

        public Producto FindProducto(int id) {
            return this.GetProductos().FirstOrDefault(p => p.IdProducto == id);
        }
    }
}
