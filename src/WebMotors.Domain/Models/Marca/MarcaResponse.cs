using Newtonsoft.Json;

namespace WebMotors.Domain.Models.Marca
{
    public class MarcaResponse
    {
        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Nome { get; set; }
    }
}
