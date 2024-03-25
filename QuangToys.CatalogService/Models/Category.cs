using Newtonsoft.Json;
using QuangToys.Common.Interfaces;

namespace QuangToys.CatalogService.Models
{
    public class Category: IEntity
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("products")]
        public ICollection<Product> Products { get; set; }
    }
}
