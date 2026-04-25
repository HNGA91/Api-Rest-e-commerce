# Api-Rest-e-commerce

API REST en ASP.NET Core 8 avec client MVC consommateur, développée dans le cadre d'un TP scolaire.

---

## Stack technique

**UserAPI** — API REST

- ASP.NET Core 8 Web API
- Entity Framework Core 8
- SQL Server LocalDB
- Swagger / OpenAPI

**UserClient** — Client MVC

- ASP.NET Core 8 MVC
- HttpClient (consommateur de l'API)
- Razor Views
- Bootstrap 5

---

## Architecture

```
UserClient (MVC)
      ↓ HttpClient
UserAPI (REST)
      ↓ Entity Framework
SQL Server LocalDB
```

Le client MVC ne communique jamais directement avec la base de données.
Toutes les opérations passent par l'API REST.

---

## Structure du projet

```
Api-Rest-e-commerce/
├── UserAPI/
│   ├── Controllers/
│   │   ├── UsersController.cs
│   │   └── ProduitsController.cs
│   ├── Data/
│   │   └── AppDbContext.cs
│   ├── DTOs/
│   │   ├── CreateUserDto.cs
│   │   ├── UpdateUserDto.cs
│   │   ├── LoginRequestDto.cs
│   │   ├── UserResponseDto.cs
│   │   ├── CreateProduitDto.cs
│   │   ├── UpdateProduitDto.cs
│   │   └── ProduitResponseDto.cs
│   ├── Models/
│   │   ├── User.cs
│   │   └── Produit.cs
│   └── Services/
│       ├── IUserService.cs
│       ├── UserService.cs
│       ├── IProduitService.cs
│       └── ProduitService.cs
└── UserClient/
    ├── Controllers/
    │   ├── UsersController.cs
    │   └── ProduitsController.cs
    ├── Models/
    │   ├── UserViewModel.cs
    │   └── ProduitViewModel.cs
    └── Views/
        ├── Users/
        │   ├── Index.cshtml
        │   ├── Details.cshtml
        │   ├── Create.cshtml
        │   ├── Edit.cshtml
        │   ├── Delete.cshtml
        │   └── Login.cshtml
        └── Produits/
            ├── Index.cshtml
            ├── Details.cshtml
            ├── Create.cshtml
            ├── Edit.cshtml
            └── Delete.cshtml
```

---

## Fonctionnalités

### Gestion des utilisateurs

| Méthode | Route              | Description                 |
| ------- | ------------------ | --------------------------- |
| GET     | `/api/Users`       | Liste tous les utilisateurs |
| GET     | `/api/Users/{id}`  | Détails d'un utilisateur    |
| POST    | `/api/Users`       | Créer un utilisateur        |
| POST    | `/api/Users/login` | Authentification            |
| PUT     | `/api/Users/{id}`  | Modifier un utilisateur     |
| DELETE  | `/api/Users/{id}`  | Supprimer un utilisateur    |

### Gestion des produits

| Méthode | Route                | Description             |
| ------- | -------------------- | ----------------------- |
| GET     | `/api/Produits`      | Liste tous les produits |
| GET     | `/api/Produits/{id}` | Détails d'un produit    |
| POST    | `/api/Produits`      | Créer un produit        |
| PUT     | `/api/Produits/{id}` | Modifier un produit     |
| DELETE  | `/api/Produits/{id}` | Supprimer un produit    |

---

## Prérequis

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022
- SQL Server LocalDB (inclus avec Visual Studio)

---

## Installation et lancement

### 1. Cloner le dépôt

```bash
git clone https://github.com/HNGA91/Api-Rest-e-commerce.git
cd Api-Rest-e-commerce
```

### 2. Configurer la base de données

Dans `UserAPI/appsettings.json`, adapter la connection string à votre environnement :

```json
{
	"ConnectionStrings": {
		"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TP_AspNetCore;Trusted_Connection=True;"
	}
}
```

### 3. Appliquer les migrations

```bash
cd UserAPI
dotnet ef database update
```

### 4. Lancer les deux projets

**Option A — Visual Studio**

- Clic droit sur la Solution → Définir les projets de démarrage
- Sélectionner "Plusieurs projets de démarrage"
- Mettre UserAPI et UserClient sur "Démarrer"
- Lancer avec ▶️

**Option B — Terminal**

```bash
# Terminal 1
cd UserAPI
dotnet run

# Terminal 2
cd UserClient
dotnet run
```

### 5. Accéder aux applications

| Application   | URL                            |
| ------------- | ------------------------------ |
| Swagger (API) | https://localhost:7104/swagger |
| Client MVC    | https://localhost:44307        |

---

## Règles de validation

### Utilisateur

- Nom et prénom : minimum 2 caractères, lettres uniquement
- Email : format RFC valide, unique en base
- Téléphone : exactement 10 chiffres (format français)
- Mot de passe : minimum 12 caractères, avec au moins une majuscule, une minuscule, un chiffre et un caractère spécial

### Produit

- Nom : maximum 100 caractères, sans caractères `<` et `>`
- Prix : strictement supérieur à 0
- Stock : positif ou nul

---

## Patterns utilisés

- **Repository pattern** via Entity Framework DbContext
- **Service layer** — toute la logique métier dans les services
- **Interface** — couplage faible entre contrôleurs et services
- **DTO** — séparation entre les modèles de données et les données exposées
- **TempData** — transmission de données entre deux requêtes HTTP sans appel BDD supplémentaire
