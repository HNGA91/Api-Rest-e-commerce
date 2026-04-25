using System.ComponentModel.DataAnnotations;

namespace UserAPI.DTOs
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "L'email n'est pas valide.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{12,}$",
            ErrorMessage = "Le mot de passe doit contenir au moins 12 caractères, une majuscule, une minuscule, un chiffre et un caractère spécial.")]
        public string MotDePasse { get; set; } = string.Empty;
    }
}