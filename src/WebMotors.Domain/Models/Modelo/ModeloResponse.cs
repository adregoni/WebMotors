using Newtonsoft.Json;

namespace WebMotors.Domain.Models.Modelo
{
    public class ModeloResponse
    {
        [JsonProperty("MakeID")]
        public int MarcaId { get; set; }

        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Nome { get; set; }
    }
}
