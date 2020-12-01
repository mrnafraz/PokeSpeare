using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using PokeSpeare.Service.Controllers;
using PokeSpeare.Service.Models;
using PokeSpeare.Service.Services;

namespace PokeSpeare.Service.UnitTests
{
    public class PokemonControllerTests
    {
        private PokemonController _pokemonController;

        [SetUp]
        public void Setup()
        {
            var mockPokemonService = new Mock<IPokemonService>();
            var mockShakespeareTranslationService = new Mock<IShakespeareTranslationService>();
            mockPokemonService.Setup(x => x.GetPokemon(Constants.ValidPokemon)).ReturnsAsync(new PokemonInfo());
            mockPokemonService.Setup(x => x.GetPokemon(Constants.InvalidPokemon)).ReturnsAsync(default(PokemonInfo));
            _pokemonController = new PokemonController(mockPokemonService.Object, mockShakespeareTranslationService.Object, NullLogger<PokemonController>.Instance);
        }

        [Test]
        public void ShouldReturnOKForValidPokemon()
        {
            var result = _pokemonController.Get(Constants.ValidPokemon);

            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void ShouldReturnNotFoundForInvalidPokemon()
        {
            var result = _pokemonController.Get(Constants.InvalidPokemon);

            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);
        }
    }
}