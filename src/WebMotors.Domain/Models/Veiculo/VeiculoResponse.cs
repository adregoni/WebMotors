using Newtonsoft.Json;

namespace WebMotors.Domain.Models.Veiculo
{
    public class VeiculoResponse
    {
        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("Make")]
        public string Marca { get; set; }

        [JsonProperty("Model")]
        public string Modelo { get; set; }

        [JsonProperty("Version")]
        public string Versao { get; set; }

        [JsonProperty("Image")]
        public string Imagem { get; set; }

        [JsonProperty("KM")]
        public string Quilometragem { get; set; }

        [JsonProperty("Price")]
        public string Preco { get; set; }

        [JsonProperty("YearModel")]
        public string AnoModelo { get; set; }

        [JsonProperty("YearFab")]
        public string AnoFabricacao { get; set; }

        [JsonProperty("Color")]
        public string Cor { get; set; }

    }
}
