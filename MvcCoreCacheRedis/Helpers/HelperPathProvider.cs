namespace MvcCoreCacheRedis.Helpers
{
    public enum Folders { images, documents }
    public class HelperPathProvider
    {
        private readonly IWebHostEnvironment env;

        public HelperPathProvider(IWebHostEnvironment env) {
            this.env = env;
        }

        public string MapPath(Folders folder, string fileName) {
            string carpeta = "";
            switch (folder) {
                case Folders.images:
                    carpeta = "images";
                    break;
                case Folders.documents:
                    carpeta = "documents";
                    break;
            }
            string path = Path.Combine(this.env.WebRootPath, carpeta, fileName);
            return path;
        }
    }
}
