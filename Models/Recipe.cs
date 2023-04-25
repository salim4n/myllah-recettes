using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace client.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }
        public string UserName { get; set; }

    }

    public class CreateRecipeModel
    {
        [Required(ErrorMessage = "Le Nom est obligatoire.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "La Description est obligatoire.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Une Image est obligatoire.")]
        public IBrowserFile File { get; set; }
        public string UserName { get; set; } = "Anonyme";

    }

    public class UpdateRecipe
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IBrowserFile? File { get; set; }

    }
}
