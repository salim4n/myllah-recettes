using client.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace client.Service.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private readonly string baseUri = "http://localhost:7214/api/";
        private readonly HttpClient _http;

        public RecipeService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Recipe> CreateRecipe(CreateRecipeModel recipe)
        {
            var formContent = new MultipartFormDataContent
            {
                { new StringContent(recipe.Title), "title" },
                { new StringContent(recipe.Description), "description" },
                { new StreamContent(recipe.File.OpenReadStream()), "file", recipe.File.Name },
                { new StringContent(recipe.UserName), "UserName" }
            };

            var result = await _http.PostAsync($"{baseUri}Recipes", formContent);
            var recipeCreated = await result.Content.ReadFromJsonAsync<Recipe>();
            return recipeCreated;
        }

        public async Task<bool> DeleteRecipe(Recipe recipe)
        {
           var result =  await _http.DeleteAsync($"{baseUri}recipes/{recipe.Id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<List<Recipe>> GetAllRecipe()
        {
            var recipes = await _http.GetFromJsonAsync<List<Recipe>>($"{baseUri}Recipes");
            return recipes is not null ? recipes : new List<Recipe>();
        }

        public async Task<Recipe> GetRecipeById(string id)
        {
            var recipe = await _http.GetFromJsonAsync<Recipe>($"{baseUri}recipes/{id}");
            return recipe is not null ? recipe : (recipe = new());
        }

        public async Task<Recipe> UpdateRecipe(UpdateRecipeModel recipe)
        {
           
            try
            {
                var formContent = new MultipartFormDataContent();
                if(recipe.Title != string.Empty)
                {
                    formContent.Add(new StringContent(recipe.Title), "title");
                }
                if (recipe.Description != string.Empty)
                {
                    formContent.Add(new StringContent(recipe.Description), "description");
                }
                if(recipe.File is not null)
                {
                    formContent.Add(new StreamContent(recipe.File.OpenReadStream()), "file", recipe.File.Name);
                }
                
                var result = await _http.PutAsync($"{baseUri}recipes/{recipe.Id}", formContent);

                if (result.IsSuccessStatusCode)
                {
                    var recipeUpdated = await result.Content.ReadFromJsonAsync<Recipe>();
                    return recipeUpdated;
                }
                else
                {
                    return new Recipe();
                }
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            
          
        }
    }
}
