using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMotors.Domain.Contracts.OnlineChallenge;
using WebMotors.Domain.Models.Marca;
using WebMotors.Domain.Models.Modelo;
using WebMotors.Domain.Models.Versao;

namespace WebMotors.Anticorruption.OnlineChallenge.Adapters
{
    public class OnlineChallengerAdapter : IOnlineChallengeAdapter
    {
        private readonly string _url;
        private readonly ILogger<OnlineChallengerAdapter> _logger;

        public OnlineChallengerAdapter(IConfiguration configuration, ILogger<OnlineChallengerAdapter> logger)
        {
            _url = configuration.GetSection("EndPoints:OnlineChallenge").Value;
            _logger = logger;
        }
        
        public async Task<List<MarcaResponse>> ObterMarcas()
        {
            try
            {
                using (var fc = new FlurlClient())
                {
                    var reponse = await $"{_url}/Make".
                        WithClient(fc).GetJsonAsync<List<MarcaResponse>>();

                    return reponse;
                }
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseStringAsync();
                _logger.LogError(error);
                return null;
            }
        }

        public async Task<List<ModeloResponse>> ObterModelos(int marcaId)
        {
            try
            {
                using (var fc = new FlurlClient())
                {
                    var reponse = await $"{_url}/Model"
                        .WithClient(fc)
                        .SetQueryParam("MakeID", marcaId)
                        .GetJsonAsync<List<ModeloResponse>>();

                    return reponse;
                }
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseStringAsync();
                _logger.LogError(error);
                return null;
            }
        }

        public async Task<List<VersaoResponse>> ObterVersoes(int modeloId)
        {
            try
            {
                using (var fc = new FlurlClient())
                {
                    var reponse = await $"{_url}/Version"
                        .WithClient(fc)
                        .SetQueryParam("ModelID", modeloId)
                        .GetJsonAsync<List<VersaoResponse>>();

                    return reponse;
                }
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseStringAsync();
                _logger.LogError(error);
                return null;
            }
        }
    }
}
