using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using UserAPI.DTOs;
using UserAPI.Models;

namespace UserAPI.Services
{
    public class ProduitService : IProduitService
    {
        private readonly AppDbContext _context;

        public ProduitService(AppDbContext context)
        {
            _context = context;
        }

        // Récupérer tous les produits
        public async Task<IEnumerable<ProduitResponseDto>> GetAllProduits()
        {
            return await _context.Produits.Select(p => new ProduitResponseDto
            {
                Id = p.Id,
                Nom = p.Nom,
                Prix = p.Prix,
                Stock = p.Stock
            }).ToListAsync();
        }

        // Récupérer un produit par Id
        public async Task<ProduitResponseDto?> GetProduitById(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null) return null;

            return new ProduitResponseDto
            {
                Id = produit.Id,
                Nom = produit.Nom,
                Prix = produit.Prix,
                Stock = produit.Stock
            };
        }

        // Créer un produit
        public async Task<ProduitResponseDto> CreerProduit(CreateProduitDto dto)
        {
            var produit = new Produit(dto.Nom, dto.Prix, dto.Stock);

            produit.ChangerNom(dto.Nom);
            produit.ChangerPrix(dto.Prix);
            produit.ChangerStock(dto.Stock);

            _context.Produits.Add(produit);
            await _context.SaveChangesAsync();

            return new ProduitResponseDto
            {
                Id = produit.Id,
                Nom = produit.Nom,
                Prix = produit.Prix,
                Stock = produit.Stock
            };
        }

        // Modifier un produit
        public async Task<ProduitResponseDto?> UpdateProduit(int id, UpdateProduitDto dto)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null) return null;

            produit.ChangerNom(dto.Nom);
            produit.ChangerPrix(dto.Prix);
            produit.ChangerStock(dto.Stock);

            await _context.SaveChangesAsync();

            return new ProduitResponseDto
            {
                Id = produit.Id,
                Nom = produit.Nom,
                Prix = produit.Prix,
                Stock = produit.Stock
            };
        }

        // Supprimer un produit
        public async Task<bool> SupprimerProduit(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null) return false;

            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}