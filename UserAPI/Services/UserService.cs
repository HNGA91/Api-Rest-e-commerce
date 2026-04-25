using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using UserAPI.DTOs;
using UserAPI.Models;

namespace UserAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Vérifier si l'email existe
        public async Task<bool> EmailExiste(string email)
        {
            email = email?.Trim().ToLower();
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email);
        }

        // Récupérer tous les utilisateurs
        public async Task<IEnumerable<UserResponseDto>> GetAllUsers()
        {
            return await _context.Users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Nom = u.Nom,
                Prenom = u.Prenom,
                Email = u.Email,
                Telephone = u.Telephone,
                Role = u.Role
            }).ToListAsync();
        }

        // Récupérer un utilisateur par Id
        public async Task<UserResponseDto?> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Nom = user.Nom,
                Prenom = user.Prenom,
                Email = user.Email,
                Telephone = user.Telephone,
                Role = user.Role
            };
        }

        // Créer un utilisateur
        public async Task<UserResponseDto> CreerUser(CreateUserDto dto)
        {
            var emailClean = dto.Email?.Trim().ToLower();

            if (await EmailExiste(emailClean))
                throw new ArgumentException("Cet email est déjà utilisé.");

            var user = new User(dto.Nom, dto.Prenom, emailClean, dto.Telephone, dto.MotDePasse);

            user.ChangerNom(dto.Nom);
            user.ChangerPrenom(dto.Prenom);
            user.ChangerEmail(emailClean);
            user.ChangerTelephone(dto.Telephone);
            user.ChangerMotDePasse(dto.MotDePasse);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Nom = user.Nom,
                Prenom = user.Prenom,
                Email = user.Email,
                Telephone = user.Telephone,
                Role = user.Role
            };
        }

        // Modifier un utilisateur
        public async Task<UserResponseDto?> UpdateUser(int id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.ChangerNom(dto.Nom);
            user.ChangerPrenom(dto.Prenom);
            user.ChangerEmail(dto.Email);
            user.ChangerTelephone(dto.Telephone);

            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Nom = user.Nom,
                Prenom = user.Prenom,
                Email = user.Email,
                Telephone = user.Telephone,
                Role = user.Role
            };
        }

        // Supprimer un utilisateur
        public async Task<bool> SupprimerUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Login
        public async Task<UserResponseDto?> Login(LoginRequestDto dto)
        {
            var email = dto.Email?.Trim().ToLower();
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email);

            if (user == null) return null;

            bool ok = dto.MotDePasse == user.MotDePasse;
            if (!ok) return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Nom = user.Nom,
                Prenom = user.Prenom,
                Email = user.Email,
                Telephone = user.Telephone,
                Role = user.Role
            };
        }
    }
}