using PracticaArquitectura.Entities;
using PracticaArquitectura.Models.DTOs.Reponses;
using PracticaArquitectura.Models.DTOs.Requests;
using PracticaArquitectura.Repositories.Interfaces;
using PracticaArquitectura.Services.Interfaces;

namespace PracticaArquitectura.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public List<ProductForReadDto> GetAllProducts()
        {
            List<Product> productsEntidad = _repository.GetAllProducts();
            List<ProductForReadDto> productsDto = productsEntidad.Select(u => new ProductForReadDto
            {
                Id = u.Id,
                Name = u.Name,
                Price = u.Price,
            }).ToList();
            return productsDto;
        }
        public ProductForReadDto? GetProductById(int id)
        {
            Product? ProductEntidad = _repository.GetProductById(id);
            if (ProductEntidad == null)
            {
                return null;
            }
            return new ProductForReadDto
            {
                Id = ProductEntidad.Id,
                Name = ProductEntidad.Name,
                Price = ProductEntidad.Price,
            };
        }
        public ProductForReadDto CreateProduct(ProductForCreateDto dto)
        {
            Product newProduct = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
            };

            _repository.AddProduct(newProduct);

            return new ProductForReadDto
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Price = newProduct.Price,
            };

        }
        public void UpdateProduct(int id, ProductForUpdateDto dto)
        {
            Product? existingProduct = _repository.GetProductById(id);

            if (existingProduct == null)
            {
                return;
            }

            existingProduct.Name = dto.Name;
            existingProduct.Price = dto.Price;

            _repository.UpdateProduct(existingProduct);
        }
        public void DeleteProduct(int id)
        {
            Product? ProductEntidad = _repository.GetProductById(id);
            if (ProductEntidad == null)
            {
                return;
            }

            _repository.DeleteProduct(ProductEntidad);
        }
        public List<ProductForReadDto> SearchProductsByName(string name)
        {
            var entidades = _repository.SearchProductsByName(name);

            return entidades.Select(p => new ProductForReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }
        public ProductStatsDto GetStats()
        {
            var productos = _repository.GetAllProducts();

            if (productos.Count == 0)
            {
                return new ProductStatsDto { Total = 0, AveragePrice = 0, MostExpensiveName = "Sin productos" };
            }

            var total = productos.Count();
            var promedio = productos.Average(p => p.Price);

            var masCaro = productos.OrderByDescending(p => p.Price).First();

            return new ProductStatsDto
            {
                Total = total,
                AveragePrice = promedio,
                MostExpensiveName = masCaro.Name
            };
        }
            public bool NameExists(string name)
        {
            return _repository.GetAllProducts().Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}