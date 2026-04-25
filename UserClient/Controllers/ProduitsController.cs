using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using UserClient.Models;

namespace UserClient.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProduitsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient GetClient() => _httpClientFactory.CreateClient("UserAPI");

        // GET: /Produits
        public async Task<IActionResult> Index()
        {
            var client = GetClient();
            var response = await client.GetAsync("Produits");

            if (!response.IsSuccessStatusCode)
                return View(new List<ProduitViewModel>());

            var json = await response.Content.ReadAsStringAsync();
            var produits = JsonSerializer.Deserialize<List<ProduitViewModel>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(produits);
        }

        // GET: /Produits/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var client = GetClient();
            var response = await client.GetAsync($"Produits/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var produit = JsonSerializer.Deserialize<ProduitViewModel>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(produit);
        }

        // GET: /Produits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Produits/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateProduitViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = GetClient();
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Produits", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Erreur lors de la création.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Produits/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var client = GetClient();
            var response = await client.GetAsync($"Produits/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var produit = JsonSerializer.Deserialize<UpdateProduitViewModel>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(produit);
        }

        // POST: /Produits/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateProduitViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = GetClient();
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"Produits/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Erreur lors de la modification.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Produits/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetClient();
            var response = await client.GetAsync($"Produits/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var produit = JsonSerializer.Deserialize<ProduitViewModel>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            TempData["ProduitNom"] = produit?.Nom;
            TempData["ProduitPrix"] = produit?.Prix.ToString();
            TempData["ProduitStock"] = produit?.Stock.ToString();

            return View(produit);
        }

        // POST: /Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetClient();
            var response = await client.DeleteAsync($"Produits/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}