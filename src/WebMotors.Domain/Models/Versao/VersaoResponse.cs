using Newtonsoft.Json;

namespace WebMotors.Domain.Models.Versao
{
    public class VersaoResponse
    {
        [JsonProperty("ModelID")]
        public int ModeloId { get; set; }

        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Nome { get; set; }
    }
}
