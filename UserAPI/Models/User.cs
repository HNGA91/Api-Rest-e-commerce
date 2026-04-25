using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace UserAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [MinLength(2, ErrorMessage = "Le nom doit contenir au moins 2 caractères.")]
        public string Nom { get; private set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [MinLength(2, ErrorMessage = "Le prénom doit contenir au moins 2 caractères.")]
        public string Prenom { get; private set; }

        [Required(ErrorMessage = "L'email est obligatoire.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "L'email n'est pas valide.")]
        public string Email { get; private set; }

        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Le numéro de téléphone doit contenir 10 chiffres.")]
        public string Telephone { get; private set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{12,}$", ErrorMessage = "Le mot de passe doit contenir au moins 12 caractères, une majuscule, une minuscule, un chiffre et un caractère spécial.")]
        public string MotDePasse { get; private set; }

        public string Role { get; private set; }

        // Constructeur
        public User(string nom, string prenom, string email, string telephone, string motDePasse)
        {
            Nom = nom;
            Prenom = prenom;
            Email = email;
            Telephone = telephone;
            MotDePasse = motDePasse;
            Role = "Client";
        }

        public void ChangerNom(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
                throw new ArgumentException("Le nom est obligatoire.");
            if (nom.Length < 2)
                throw new ArgumentException("Le nom doit contenir au moins 2 caractères.");
            Nom = nom;
        }

        public void ChangerPrenom(string prenom)
        {
            if (string.IsNullOrWhiteSpace(prenom))
                throw new ArgumentException("Le prénom est obligatoire.");
            if (prenom.Length < 2)
                throw new ArgumentException("Le prénom doit contenir au moins 2 caractères.");
            Prenom = prenom;
        }

        public void ChangerEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("L'email est obligatoire.");
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!regex.IsMatch(email))
                throw new ArgumentException("L'email n'est pas valide.");
            Email = email;
        }

        public void ChangerTelephone(string telephone)
        {
            if (string.IsNullOrWhiteSpace(telephone))
                throw new ArgumentException("Le téléphone est obligatoire.");
            var regex = new Regex(@"^[0-9]{10}$");
            if (!regex.IsMatch(telephone))
                throw new ArgumentException("Le numéro doit contenir 10 chiffres.");
            Telephone = telephone;
        }

        public void ChangerMotDePasse(string motDePasse)
        {
            if (string.IsNullOrWhiteSpace(motDePasse))
                throw new ArgumentException("Le mot de passe est obligatoire.");
            var regex = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{12,}$");
            if (!regex.IsMatch(motDePasse))
                throw new ArgumentException("Le mot de passe doit contenir au moins 12 caractères, une majuscule, une minuscule et un caractère spécial.");
            MotDePasse = motDePasse;
        }

        public void ChangerRole(string role)
        {
            Role = role;
        }
    }
}