namespace Web.UI.Models
{
    public class AnuncioAtualizarViewModel
    {
        public int Id { get; set; }

        public int Ano { get; set; }

        public int Quilometragem { get; set; }

        public string Observacao { get; set; }

        public void set(int id, int ano, int quilometragem, string observacao)
        {
            Id = id;
            Ano = ano;
            Quilometragem = quilometragem;
            Observacao = observacao;
        }
    }
}
