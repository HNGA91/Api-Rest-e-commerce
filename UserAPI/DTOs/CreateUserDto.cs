using System.ComponentModel.DataAnnotations;

namespace UserAPI.DTOs
{
    public class CreateUserDto
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

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{12,}$",
            ErrorMessage = "Le mot de passe doit contenir au moins 12 caractères, une majuscule, une minuscule, un chiffre et un caractère spécial.")]
        public string MotDePasse { get; set; } = string.Empty;

        [Required(ErrorMessage = "La confirmation du mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        [Compare("MotDePasse", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string ConfirmMotDePasse { get; set; } = string.Empty;
    }
}