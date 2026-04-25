using System.ComponentModel.DataAnnotations;

namespace UserAPI.DTOs
{
    public class UpdateProduitDto
    {
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le nom ne doit pas dépasser 100 caractères.")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Les caractères < et > sont interdits.")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prix est requis.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0.")]
        [DataType(DataType.Currency)]
        public decimal Prix { get; set; }

        [Required(ErrorMessage = "Le stock est requis.")]
        [Range(0, int.MaxValue, ErrorMessage = "Le stock doit être positif.")]
        public int Stock { get; set; }
    }
}