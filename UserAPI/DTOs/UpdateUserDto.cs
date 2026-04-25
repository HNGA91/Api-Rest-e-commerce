using System.ComponentModel.DataAnnotations;

namespace UserAPI.DTOs
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [MinLength(2, ErrorMessage = "Le nom doit contenir au moins 2 caractères.")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [MinLength(2, ErrorMessage = "Le prénom doit contenir au moins 2 caractères.")]
        public string Prenom { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "L'email n'est pas valide.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Le numéro doit contenir 10 chiffres.")]
        public string Telephone { get; set; } = string.Empty;
    }
}