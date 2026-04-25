using Microsoft.AspNetCore.Mvc;
using UserAPI.DTOs;
using UserAPI.Services;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound("Utilisateur introuvable.");
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var user = await _userService.CreerUser(dto);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userService.Login(dto);
            if (user == null) return Unauthorized("Email ou mot de passe incorrect.");
            return Ok(user);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userService.UpdateUser(id, dto);
            if (user == null) return NotFound("Utilisateur introuvable.");
            return Ok(user);
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.SupprimerUser(id);
            if (!result) return NotFound("Utilisateur introuvable.");
            return NoContent();
        }
    }
}