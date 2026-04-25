using UserAPI.DTOs;

namespace UserAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsers();
        Task<UserResponseDto?> GetUserById(int id);
        Task<UserResponseDto> CreerUser(CreateUserDto dto);
        Task<UserResponseDto?> UpdateUser(int id, UpdateUserDto dto);
        Task<bool> SupprimerUser(int id);
        Task<UserResponseDto?> Login(LoginRequestDto dto);
        Task<bool> EmailExiste(string email);
    }
}