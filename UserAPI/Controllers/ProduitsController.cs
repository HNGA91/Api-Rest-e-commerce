using Microsoft.AspNetCore.Mvc;
using UserAPI.DTOs;
using UserAPI.Services;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProduitsController : ControllerBase
    {
        private readonly IProduitService _produitService;

        public ProduitsController(IProduitService produitService)
        {
            _produitService = produitService;
        }

        // GET: api/produits
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produits = await _produitService.GetAllProduits();
            return Ok(produits);
        }

        // GET: api/produits/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produit = await _produitService.GetProduitById(id);
            if (produit == null) return NotFound("Produit introuvable.");
            return Ok(produit);
        }

        // POST: api/produits
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProduitDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var produit = await _produitService.CreerProduit(dto);
                return CreatedAtAction(nameof(GetById), new { id = produit.Id }, produit);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/produits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProduitDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var produit = await _produitService.UpdateProduit(id, dto);
            if (produit == null) return NotFound("Produit introuvable.");
            return Ok(produit);
        }

        // DELETE: api/produits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _produitService.SupprimerProduit(id);
            if (!result) return NotFound("Produit introuvable.");
            return NoContent();
        }
    }
}