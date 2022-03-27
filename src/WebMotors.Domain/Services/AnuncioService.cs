using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMotors.Domain.Contracts;
using WebMotors.Domain.Contracts.OnlineChallenge;
using WebMotors.Domain.Contracts.Repository;
using WebMotors.Domain.Contracts.UnityOfWork;
using WebMotors.Domain.Core.Exceptions;
using WebMotors.Domain.Entities;
using WebMotors.Domain.Models;
using WebMotors.Domain.Models.Anuncio;
using WebMotors.Domain.Models.Marca;
using WebMotors.Domain.Models.Modelo;
using WebMotors.Domain.Models.Versao;

namespace WebMotors.Domain.Services
{
    public class AnuncioService : IAnuncioService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unityOfWork;
        private readonly IOnlineChallengeAdapter _onlineChallengeAdapter;
        private readonly IRepository<Anuncio> _anuncioRepository;

        public AnuncioService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IOnlineChallengeAdapter onlineChallengeAdapter,
            IRepository<Anuncio> anuncioRepository)
        {
            _mapper = mapper;
            _unityOfWork = unitOfWork;
            _onlineChallengeAdapter = onlineChallengeAdapter;
            _anuncioRepository = anuncioRepository;
        }

        public async Task<AnuncioResponse> Adicionar(AnuncioRequest anuncio)
        {
            if (anuncio.Id > 0)
            {
                throw new DomainException("The field id must not be greater than 0.");
            }

            var entity = await _anuncioRepository.AddAsync(new Anuncio());

            _mapper.Map(anuncio, entity);

            await _unityOfWork.CommitAsync();

            var result = _mapper.Map<AnuncioResponse>(entity);

            return result;

        }

        public async Task<AnuncioResponse> Atualizar(AnuncioAtualizarRequest anuncio)
        {
            if (anuncio.Id < 1)
            {
                throw new DomainException("The field id must be greater than 0.");
            }

            var entity = await _anuncioRepository.FirstOrDefaultAsync(x => x.Id.Equals(anuncio.Id));

            entity.atualizar(anuncio.Ano, anuncio.Quilometragem, anuncio.Observacao);
          
            await _anuncioRepository.UpdateAsync(entity);

            await _unityOfWork.CommitAsync();

            return _mapper.Map<AnuncioResponse>(entity);
        }

        public async Task<List<MarcaResponse>> ObterMarcas()
        {
            var marcas = await _onlineChallengeAdapter.ObterMarcas();

            if (marcas is null)
            {
                throw new DomainException("Não foi possivel obter Marcas");
            }

            return marcas;
        }

        public async Task<List<ModeloResponse>> ObterModelos(int marcaId)
        {
            var modelos = await _onlineChallengeAdapter.ObterModelos(marcaId);

            if (modelos is null)
            {
                throw new DomainException("Não foi possivel obter Modelos");
            }

            return modelos;
        }

        public async Task<AnuncioResponse> ObterPorCodigo(int id)
        {
            var entity = await _anuncioRepository.FirstOrDefaultAsync(x => x.Id.Equals(id));

            return _mapper.Map<AnuncioResponse>(entity);
        }

        public async Task<List<AnuncioResponse>> ObterTodos()
        {
            var model = await _anuncioRepository.GetAllAsync();

            return _mapper.Map<List<AnuncioResponse>>(model);
        }

        public async Task<List<VersaoResponse>> ObterVersoes(int modeloId)
        {
            var versoes = await _onlineChallengeAdapter.ObterVersoes(modeloId);

            if (versoes is null)
            {
                throw new DomainException("Não foi possivel obter Versões");
            }

            return versoes;
        }

        public async Task Remover(int id)
        {
            var entity = await _anuncioRepository.FirstOrDefaultAsync(x => x.Id.Equals(id));

            await _anuncioRepository.RemoveAsync(entity);

            await _unityOfWork.CommitAsync();
        }
    }
}
