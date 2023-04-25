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
        public List<Recipe> RecipesListState { get; set; } = new List<Recipe>();
        public Recipe RecipeState { get; set; } = new Recipe();
        public UpdateRecipe UpdateRecipeState { get; set; } = new ();
        public CreateRecipe CreateRecipeState { get; set; } = new ();

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
				RecipesListState = await recipeService.GetAllRecipe();
				NotifyStateChanged();
		}

        public async Task AddRecipe(CreateRecipe recipe)
        {
			using var scope = _serviceScopeFactory.CreateScope();
			var recipeService = scope.ServiceProvider.GetRequiredService<IRecipeService>();
			var recipeToAdd = await recipeService.CreateRecipe(recipe);
            RecipesListState.Add(recipeToAdd);
            NotifyStateChanged();
        }

        public async Task DeleteRecipe(Recipe recipe)
        {
			using var scope = _serviceScopeFactory.CreateScope();
			var recipeService = scope.ServiceProvider.GetRequiredService<IRecipeService>();
			var isSuccess = await recipeService.DeleteRecipe(recipe);
            if (isSuccess)
            {
                RecipesListState.Remove(recipe);
                NotifyStateChanged();
            }     

        }

        public async Task UpdateRecipe(UpdateRecipe recipe)
        {
			using var scope = _serviceScopeFactory.CreateScope();
			var recipeService = scope.ServiceProvider.GetRequiredService<IRecipeService>();
			RecipeState = await recipeService.UpdateRecipe(recipe);
            NotifyStateChanged();
        }

        public async void GetRecipe(int id)
        {
			using var scope = _serviceScopeFactory.CreateScope();
			var recipeService = scope.ServiceProvider.GetRequiredService<IRecipeService>();

			if (RecipesListState.Count > 0)
            {
                RecipeState = RecipesListState.FirstOrDefault(r => r.Id == id);
                NotifyStateChanged();
            }
            else
            {
                RecipesListState?.Clear();
                RecipesListState = await recipeService.GetAllRecipe();
                RecipeState = RecipesListState.FirstOrDefault(r => r.Id == id);
                NotifyStateChanged();

            }
            
        }



    }
}
