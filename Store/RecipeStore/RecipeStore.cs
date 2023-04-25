using client.Models;
using client.Service.RecipeService;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
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
        public CreateRecipeModel CreateRecipeState { get; set; } = new ();
        public string ErrorMessage { get; set; } = string.Empty;

        private void NotifyStateChanged() => OnStateChange?.Invoke();


        public void SetRecipe(Recipe recipe)
        {
            ResetErrorMesage();
            RecipeState = recipe;
            NotifyStateChanged();
        }

        public async Task SetRecipesState()
        {
            ResetErrorMesage();
            using var scope = _serviceScopeFactory.CreateScope();
				var recipeService = scope.ServiceProvider.GetRequiredService<IRecipeService>();
				RecipesListState = await recipeService.GetAllRecipe();
				NotifyStateChanged();
		}

        public async Task AddRecipe(CreateRecipeModel recipe)
        {
            ResetErrorMesage();
			using var scope = _serviceScopeFactory.CreateScope();
			var recipeService = scope.ServiceProvider.GetRequiredService<IRecipeService>();
			var recipeToAdd = await recipeService.CreateRecipe(recipe);
            if (recipeToAdd.Name != string.Empty || recipeToAdd.Name != "")
            {
                RecipesListState.Add(recipeToAdd);
                NotifyStateChanged();
            }
            else
                ErrorMessage = "Echec de création de Recette";

        }

        public async Task DeleteRecipe(Recipe recipe)
        {
            ResetErrorMesage();
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
            ResetErrorMesage();
            using var scope = _serviceScopeFactory.CreateScope();
			var recipeService = scope.ServiceProvider.GetRequiredService<IRecipeService>();
			RecipeState = await recipeService.UpdateRecipe(recipe);
            NotifyStateChanged();
        }

        public async void GetRecipe(int id)
        {
            ResetErrorMesage();
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

        private void ResetErrorMesage()
        {
            if (!ErrorMessage.IsNullOrEmpty())
                ErrorMessage = string.Empty;
        }



    }
}
