using Newtonsoft.Json;
using QuangToys.Common.Interfaces;

namespace QuangToys.CatalogService.Models
{
    public class Product: IEntity
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("categoryIds")]
        public Guid[] CategoryIds { get; set; }
    }
}
