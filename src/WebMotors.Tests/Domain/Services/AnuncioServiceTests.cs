using Moq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebMotors.Domain.Contracts;
using WebMotors.Domain.Contracts.Repository;
using WebMotors.Domain.Contracts.UnityOfWork;
using WebMotors.Domain.Entities;
using WebMotors.Domain.Contracts.OnlineChallenge;
using WebMotors.Domain.Services;
using System.Threading.Tasks;
using WebMotors.Tests.Support;
using WebMotors.Domain.Core.Exceptions;

namespace WebMotors.Tests.Domain.Services
{
    [TestClass]
    public class AnuncioServiceTests
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IRepository<Anuncio>> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unityOfWorkMock;
        private readonly Mock<IOnlineChallengeAdapter> _onlineChallengeAdapter;

        private readonly IAnuncioService _anuncioService;

        public AnuncioServiceTests()
        {
            _mapper = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepository<Anuncio>>();
            _unityOfWorkMock = new Mock<IUnitOfWork>();
            _onlineChallengeAdapter = new Mock<IOnlineChallengeAdapter>();

            _anuncioService = new AnuncioService(
               _mapper.Object,
               _unityOfWorkMock.Object,
               _onlineChallengeAdapter.Object,
              _repositoryMock.Object);
        }

        [TestMethod]
        public async Task Add_WhenBeCalledWithIdGreaterThanZero_ThenShouldThrowsDomainException()
        {
            // Arrange
            var anuncio = AnuncioFixture.GetAnuncio();
            anuncio.Id = 1;

            // Action
            var result = await Assert.ThrowsExceptionAsync<DomainException>(async () => await _anuncioService.Adicionar(anuncio));

            // Assert
            Assert.IsTrue(result is DomainException);
        }

        [TestMethod]
        public async Task Add_WhenBeCalledWithIdGreaterThanZero_ThenShouldThrowsDomainExceptionWithMessage()
        {
            // Arrange
            var expectedErrorMessage = "The field id must not be greater than 0.";
            var anuncio = AnuncioFixture.GetAnuncio();
            anuncio.Id = 10;

            // Action
            var result = await Assert.ThrowsExceptionAsync<DomainException>(async () => await _anuncioService.Adicionar(anuncio));

            // Assert
            Assert.AreEqual(expectedErrorMessage, result.Message);
        }

        [TestMethod]
        public async Task Add_WhenBeCalledWithIdEqualsZero_ThenShouldCallAddMethodOnce()
        {
            // Arrange
            var anuncio = AnuncioFixture.GetAnuncio();
            anuncio.Id = default;
            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<Anuncio>(), default)).ReturnsAsync(AnuncioFixture.GetEntity());

            // Action
            await _anuncioService.Adicionar(anuncio);

            // Assert
            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<Anuncio>(), default), Times.Once());
        }

        [TestMethod]
        public async Task Add_WhenBeCalledWithIdEqualsZero_ThenShouldCallCommitMethodOnce()
        {
            // Arrange
            var anuncio = AnuncioFixture.GetAnuncio();
            anuncio.Id = default;
            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<Anuncio>(), default)).ReturnsAsync(AnuncioFixture.GetEntity());

            // Action
            await _anuncioService.Adicionar(anuncio);

            // Assert
            _unityOfWorkMock.Verify(x => x.CommitAsync(), Times.Once());
        }


        [TestMethod]
        public async Task Update_WhenBeCalledWithIdEqualsZero_ThenShouldThrowsDomainException()
        {
            // Arrange
            var anuncio = AnuncioFixture.GetAnuncio();
            anuncio.Id = 0;
            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<Anuncio>(), default)).ReturnsAsync(AnuncioFixture.GetEntity());

            // Action
            var result = await Assert.ThrowsExceptionAsync<DomainException>(async () => await _anuncioService.Atualizar(anuncio));

            // Assert
            Assert.IsTrue(result is DomainException);
        }

        [TestMethod]
        public async Task Update_WhenBeCalledWithIdEqualsZero_ThenShouldThrowsDomainExceptionWithMessage()
        {
            // Arrange
            var expectedErrorMessage = "The field id must be greater than 0.";
            var anuncio = AnuncioFixture.GetAnuncio();
            anuncio.Id = 0;
            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<Anuncio>(), default)).ReturnsAsync(AnuncioFixture.GetEntity());

            // Action
            var result = await Assert.ThrowsExceptionAsync<DomainException>(async () => await _anuncioService.Atualizar(anuncio));

            // Assert
            Assert.AreEqual(result.Message, expectedErrorMessage);
        }

    }
}
