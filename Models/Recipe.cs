using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Graph;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace client.Models
{
    public class Recipe
    {
        [JsonProperty(PropertyName = "title", Required = Required.Always)]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "description", Required = Required.Always)]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "imageUrl", Required = Required.Always)]
        public string ImageUrl { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);

    }

    public class CreateRecipeModel
    {
        [Required(ErrorMessage = "Le Nom est obligatoire.")]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "La Description est obligatoire.")]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Une Image est obligatoire.")]
        [JsonProperty(PropertyName = "imageUrl", NullValueHandling = NullValueHandling.Ignore)]
        public IBrowserFile File { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; } = "Anonyme";

    }

    public class UpdateRecipeModel
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string? Title { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string? Description { get; set; }
        [JsonProperty(PropertyName = "imageUrl", NullValueHandling = NullValueHandling.Ignore)]
        public IBrowserFile? File { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }

    }
}
