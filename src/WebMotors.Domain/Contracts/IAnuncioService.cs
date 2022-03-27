using System.Threading.Tasks;
using WebMotors.Domain.Models;
using System.Collections.Generic;
using WebMotors.Domain.Models.Marca;
using WebMotors.Domain.Models.Modelo;
using WebMotors.Domain.Models.Versao;
using WebMotors.Domain.Models.Anuncio;

namespace WebMotors.Domain.Contracts
{
    public interface IAnuncioService
    {
        Task<AnuncioResponse> Adicionar(AnuncioRequest anuncio);

        Task<AnuncioResponse> Atualizar(AnuncioAtualizarRequest anuncio);

        Task<List<AnuncioResponse>> ObterTodos();

        Task<AnuncioResponse> ObterPorCodigo(int id);

        Task<List<MarcaResponse>> ObterMarcas();

        Task<List<ModeloResponse>> ObterModelos(int marcaId);

        Task<List<VersaoResponse>> ObterVersoes(int modeloId);

        Task Remover(int id);
    }
}
