using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAPI.Models
{
    public class Produit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public string Nom { get; private set; }
        public decimal Prix { get; private set; }
        public int Stock { get; private set; }

        public Produit(string nom, decimal prix, int stock)
        {
            Nom = nom;
            Prix = prix;
            Stock = stock;
        }

        public void ChangerNom(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
                throw new ArgumentException("Le nom du produit est obligatoire.");
            if (nom.Length < 2)
                throw new ArgumentException("Le nom doit contenir au moins 2 caractères.");
            Nom = nom;
        }

        public void ChangerPrix(decimal prix)
        {
            if (prix <= 0)
                throw new ArgumentException("Le prix doit être supérieur à 0.");
            Prix = prix;
        }

        public void ChangerStock(int stock)
        {
            if (stock < 0)
                throw new ArgumentException("Le stock ne peut pas être négatif.");
            Stock = stock;
        }

        public void RetirerDuStock(int quantite)
        {
            if (quantite <= 0)
                throw new ArgumentException("La quantité doit être positive.");
            if (quantite > Stock)
                throw new InvalidOperationException("Stock insuffisant.");
            Stock -= quantite;
        }

        public void AjouterAuStock(int quantite)
        {
            if (quantite <= 0)
                throw new ArgumentException("La quantité doit être positive.");
            Stock += quantite;
        }
    }
}