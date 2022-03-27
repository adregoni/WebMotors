namespace WebMotors.Domain.Entities
{
    public class Anuncio : EntityBase
    {
        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Versao { get; set; }

        public int Ano { get; set; }

        public int Quilometragem { get; set; }
        
        public string Observacao { get; set; }

        public void atualizar(int ano, int quilometragem, string observacao)
        {
            Ano = ano;
            Quilometragem = quilometragem;
            Observacao = observacao;
        }
    }
}
