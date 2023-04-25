﻿using client.Models;
using Microsoft.AspNetCore.Components.Authorization;
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

        public async Task<Recipe> CreateRecipe(CreateRecipeModel recipe)
        {
            var formContent = new MultipartFormDataContent
            {
                { new StringContent(recipe.Name), "Name" },
                { new StringContent(recipe.Description), "Description" },
                { new StreamContent(recipe.File.OpenReadStream()), "file", recipe.File.Name },
                { new StringContent(recipe.UserName), "UserName" }
            };

            var result = await _http.PostAsync($"{baseUri}api/Recipe", formContent);
            var recipeCreated = await result.Content.ReadFromJsonAsync<Recipe>();
            return recipeCreated;
        }

        public async Task<bool> DeleteRecipe(Recipe recipe)
        {
           var result =  await _http.DeleteAsync($"{baseUri}api/recipe/{recipe.Id}");
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
            var recipe = await _http.GetFromJsonAsync<Recipe>($"{baseUri}api/recipe/{id}");
            if (recipe is not null)
                return recipe;
            else
                return recipe = new();
        }

        public async Task<Recipe> UpdateRecipe(UpdateRecipeModel recipe)
        {
           
            try
            {
                var formContent = new MultipartFormDataContent();
                if(recipe.Name != string.Empty)
                {
                    formContent.Add(new StringContent(recipe.Name), "Name");
                }
                if (recipe.Description != string.Empty)
                {
                    formContent.Add(new StringContent(recipe.Description), "Description");
                }
                if(recipe.File is not null)
                {
                    formContent.Add(new StreamContent(recipe.File.OpenReadStream()), "file", recipe.File.Name);
                }

                var result = await _http.PutAsync($"{baseUri}api/recipe/{recipe.Id}", formContent);

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
