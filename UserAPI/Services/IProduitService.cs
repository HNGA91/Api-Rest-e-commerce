using UserAPI.DTOs;

namespace UserAPI.Services
{
    public interface IProduitService
    {
        Task<IEnumerable<ProduitResponseDto>> GetAllProduits();
        Task<ProduitResponseDto?> GetProduitById(int id);
        Task<ProduitResponseDto> CreerProduit(CreateProduitDto dto);
        Task<ProduitResponseDto?> UpdateProduit(int id, UpdateProduitDto dto);
        Task<bool> SupprimerProduit(int id);
    }
}