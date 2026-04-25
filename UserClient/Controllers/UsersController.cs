using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using UserClient.Models;

namespace UserClient.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient GetClient() => _httpClientFactory.CreateClient("UserAPI");

        // GET: /Users — Liste tous les utilisateurs
        public async Task<IActionResult> Index()
        {
            var client = GetClient();
            var response = await client.GetAsync("Users");

            if (!response.IsSuccessStatusCode)
                return View(new List<UserViewModel>());

            var json = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserViewModel>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(users);
        }

        // GET: /Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var client = GetClient();
            var response = await client.GetAsync($"Users/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserViewModel>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(user);
        }

        // GET: /Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = GetClient();
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Users", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Erreur lors de la création.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var client = GetClient();
            var response = await client.GetAsync($"Users/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UpdateUserViewModel>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(user);
        }

        // POST: /Users/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = GetClient();
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"Users/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Erreur lors de la modification.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Users/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetClient();
            var response = await client.GetAsync($"Users/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserViewModel>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // TempData pour transmettre les infos à la vue SANS rappeler la BDD
            TempData["UserNom"] = user?.Nom;
            TempData["UserPrenom"] = user?.Prenom;
            TempData["UserEmail"] = user?.Email;
            TempData["UserId"] = user?.Id;

            return View(user);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetClient();
            var response = await client.DeleteAsync($"Users/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Users/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Users/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = GetClient();
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Users/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Email ou mot de passe incorrect.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}