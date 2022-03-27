namespace WebMotors.Domain.Models.Anuncio
{
    public class AnuncioAtualizarRequest
    {
        public int Id { get; set; }

        public int Ano { get; set; }

        public int Quilometragem { get; set; }

        public string Observacao { get; set; }
    }
}
