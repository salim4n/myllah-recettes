﻿using client.Models;
using System.Net.Http.Json;

namespace client.Service.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private readonly string baseUri = "https://myllah-recipe-api.azurewebsites.net/";
        private readonly HttpClient _http;

        public RecipeService(HttpClient http)
        {
            _http = http;
        }

        public Task<Recipe> CreateRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteRecipe(Recipe recipe)
        {
           var result =  await _http.DeleteAsync($"{baseUri}/api/recipe/{recipe.Id}");
            if(result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<List<Recipe>> GetAllRecipe()
        {
            var recipes = await _http.GetFromJsonAsync<List<Recipe>>($"{baseUri}api/Recipe");
            if(recipes is not null)
                return recipes;
            else 
                return new List<Recipe>();
        }

        public async Task<Recipe> GetRecipeById(int id)
        {
            var recipe = await _http.GetFromJsonAsync<Recipe>($"{baseUri}/api/recipe/{id}");
            if (recipe is not null)
                return recipe;
            else
                return recipe = new();
        }

        public Task<Recipe> UpdateRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }
    }
}