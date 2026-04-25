using System.ComponentModel.DataAnnotations;

namespace UserClient.Models
{
    public class ProduitViewModel
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public decimal Prix { get; set; }
        public int Stock { get; set; }
    }

    public class CreateProduitViewModel
    {
        [Display(Name = "Nom :")]
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public string Nom { get; set; } = string.Empty;

        [Display(Name = "Prix :")]
        [Required(ErrorMessage = "Le prix est obligatoire.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0.")]
        public decimal Prix { get; set; }

        [Display(Name = "Stock :")]
        [Required(ErrorMessage = "Le stock est obligatoire.")]
        [Range(0, int.MaxValue, ErrorMessage = "Le stock doit être positif.")]
        public int Stock { get; set; }
    }

    public class UpdateProduitViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nom :")]
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public string Nom { get; set; } = string.Empty;

        [Display(Name = "Prix :")]
        [Required(ErrorMessage = "Le prix est obligatoire.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0.")]
        public decimal Prix { get; set; }

        [Display(Name = "Stock :")]
        [Required(ErrorMessage = "Le stock est obligatoire.")]
        [Range(0, int.MaxValue, ErrorMessage = "Le stock doit être positif.")]
        public int Stock { get; set; }
    }
}