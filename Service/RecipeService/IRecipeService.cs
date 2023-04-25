using client.Models;

namespace client.Service.RecipeService
{
    public interface IRecipeService
    {
        public Task<List<Recipe>> GetAllRecipe();
        public Task<Recipe> CreateRecipe(CreateRecipeModel recipe);
        public Task<Recipe> UpdateRecipe(UpdateRecipeModel recipe);
        public Task<bool> DeleteRecipe(Recipe recipe);
        public Task<Recipe> GetRecipeById(int id);
    }
}
