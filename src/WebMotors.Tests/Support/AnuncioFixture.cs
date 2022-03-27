using WebMotors.Domain.Entities;
using WebMotors.Domain.Models;

namespace WebMotors.Tests.Support
{
    public static class AnuncioFixture
    {
        public static Anuncio GetEntity()
        {
            var anuncio = new Anuncio()
            {
                Id = 1,
                Marca = "Chevrolet",
                Modelo = "Onix",
                Versao = "1.5 DX 16V FLEX 4P AUTOMÁTICO",
                Ano = 2000,
                Quilometragem = 1000,
                Observacao = "Teste"
            };
           
            return anuncio;
        }

        public static AnuncioRequest GetAnuncio() => new AnuncioRequest();
    }
}
