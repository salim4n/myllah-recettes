﻿using System.ComponentModel.DataAnnotations;

namespace client.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //[ValidateNever]
        //public List<Ingredient> Ingredients { get; set; }

        //[ValidateNever]
        //public List<Instruction> Instructions { get; set; }
        public string? ImageUri { get; set; }
        public string? UserName { get; set; }

    }

    public class CreateRecipe
    {
        //[Key]
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //[ValidateNever]
        //public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        //[ValidateNever]
        //public List<Instruction> Instructions { get; set; } = new List<Instruction>();
        public FileAccess File { get; set; }
        public string? UserName { get; set; }

    }

    public class UpdateRecipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //[ValidateNever]
        //public List<Ingredient> Ingredients { get; set; }
        //[ValidateNever]
        //public List<Instruction> Instructions { get; set; }
        public FileAccess File { get; set; }

    }
}
