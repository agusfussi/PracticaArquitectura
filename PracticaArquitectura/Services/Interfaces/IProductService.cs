using PracticaArquitectura.Models.DTOs.Reponses;
using PracticaArquitectura.Models.DTOs.Requests;

namespace PracticaArquitectura.Services.Interfaces;
public interface IProductService
{
    List<ProductForReadDto> GetAllProducts();
    ProductForReadDto? GetProductById(int id);
    ProductForReadDto CreateProduct(ProductForCreateDto dto);
    void UpdateProduct(int id, ProductForUpdateDto dto);
    void DeleteProduct(int id);
    List<ProductForReadDto> SearchProductsByName(string name);
    ProductStatsDto GetStatsDto();
}
