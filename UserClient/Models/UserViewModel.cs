using System.ComponentModel.DataAnnotations;

namespace UserClient.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class CreateUserViewModel
    {
        [Display(Name = "Nom :")]
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [MinLength(2, ErrorMessage = "Le nom doit contenir au moins 2 caractères.")]
        public string Nom { get; set; } = string.Empty;

        [Display(Name = "Prénom :")]
        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [MinLength(2, ErrorMessage = "Le prénom doit contenir au moins 2 caractères.")]
        public string Prenom { get; set; } = string.Empty;

        [Display(Name = "Email :")]
        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Téléphone :")]
        [Required(ErrorMessage = "Le téléphone est obligatoire.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Le numéro doit contenir 10 chiffres.")]
        public string Telephone { get; set; } = string.Empty;

        [Display(Name = "Mot de passe :")]
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{12,}$",
            ErrorMessage = "Le mot de passe doit contenir au moins 12 caractères, une majuscule, une minuscule, un chiffre et un caractère spécial.")]
        public string MotDePasse { get; set; } = string.Empty;

        [Display(Name = "Confirmer le mot de passe :")]
        [Required(ErrorMessage = "La confirmation est obligatoire.")]
        [DataType(DataType.Password)]
        [Compare("MotDePasse", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string ConfirmMotDePasse { get; set; } = string.Empty;
    }

    public class UpdateUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nom :")]
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [MinLength(2, ErrorMessage = "Le nom doit contenir au moins 2 caractères.")]
        public string Nom { get; set; } = string.Empty;

        [Display(Name = "Prénom :")]
        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [MinLength(2, ErrorMessage = "Le prénom doit contenir au moins 2 caractères.")]
        public string Prenom { get; set; } = string.Empty;

        [Display(Name = "Email :")]
        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Téléphone :")]
        [Required(ErrorMessage = "Le téléphone est obligatoire.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Le numéro doit contenir 10 chiffres.")]
        public string Telephone { get; set; } = string.Empty;
    }

    public class LoginViewModel
    {
        [Display(Name = "Email :")]
        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Mot de passe :")]
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; } = string.Empty;
    }
}