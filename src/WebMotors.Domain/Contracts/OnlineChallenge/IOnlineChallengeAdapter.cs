using System.Collections.Generic;
using System.Threading.Tasks;
using WebMotors.Domain.Models.Marca;
using WebMotors.Domain.Models.Modelo;
using WebMotors.Domain.Models.Versao;

namespace WebMotors.Domain.Contracts.OnlineChallenge
{
    public interface IOnlineChallengeAdapter
    {
        Task<List<MarcaResponse>> ObterMarcas();

        Task<List<ModeloResponse>> ObterModelos(int marcaId);

        Task<List<VersaoResponse>> ObterVersoes(int modeloId);
    }
}
