using client.Models;
using client.Service.RecipeService;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace client.Store.RecipeStore
{
    public class RecipeStore
    {
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public RecipeStore(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public event Action? OnStateChange;
        public List<Recipe> RecipesState { get; set; } = new List<Recipe>();
        public Recipe RecipeState { get; set; } = new Recipe();

        private void NotifyStateChanged() => OnStateChange?.Invoke();

        public void SetRecipe(Recipe recipe)
        {
            RecipeState = recipe;
            NotifyStateChanged();
        }

        public async Task SetRecipesState()
        {
				using var scope = _serviceScopeFactory.CreateScope();
				var recipeService = scope.ServiceProvider.GetRequiredService<IRecipeService>();
				RecipesState = await recipeService.GetAllRecipe();
				NotifyStateChanged();
		}

        public async Task AddRecipe(Recipe recipe)
        {
			using var scope = _serviceScopeFactory.CreateScope();
			var recipeService = scope.ServiceProvider.GetRequiredService<IRecipeService>();
			var recipeToAdd = await recipeService.CreateRecipe(recipe);
            RecipesState.Add(recipeToAdd);
            NotifyStateChanged();
        }

        public async Task DeleteRecipe(Recipe recipe)
        {
			using var scope = _serviceScopeFactory.CreateScope();
			var recipeService = scope.ServiceProvider.GetRequiredService<IRecipeService>();
			var isSuccess = await recipeService.DeleteRecipe(recipe);
            if (isSuccess)
            {
                RecipesState.Remove(recipe);
                NotifyStateChanged();
            }     

        }

        public async Task UpdateRecipe(Recipe recipe)
        {
			using var scope = _serviceScopeFactory.CreateScope();
			var recipeService = scope.ServiceProvider.GetRequiredService<IRecipeService>();
			RecipeState = await recipeService.UpdateRecipe(recipe);
        }

        public async void GetRecipe(int id)
        {
			using var scope = _serviceScopeFactory.CreateScope();
			var recipeService = scope.ServiceProvider.GetRequiredService<IRecipeService>();

			if (RecipesState.Count > 0)
            {
                RecipeState = RecipesState.FirstOrDefault(r => r.Id == id);
                NotifyStateChanged();
            }
            else
            {
                RecipesState?.Clear();
                RecipesState = await recipeService.GetAllRecipe();
                RecipeState = RecipesState.FirstOrDefault(r => r.Id == id);
                NotifyStateChanged();

            }
            
        }



    }
}
