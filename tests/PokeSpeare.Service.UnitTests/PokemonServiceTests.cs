using NUnit.Framework;
using PokeApiNet;
using PokeSpeare.Service.Models;
using PokeSpeare.Service.Services;
using System.Threading.Tasks;

namespace PokeSpeare.Service.UnitTests
{
    public class PokemonServiceTests
    {
        private PokemonService pokemonService;

        [OneTimeSetUp]
        public void Setup()
        {
            pokemonService = new PokemonService(new PokeApiClient());
        }

        [Test]
        public async Task ShouldNotReturnNullForValidPokemon()
        {
            var result = await pokemonService.GetPokemon(Constants.ValidPokemon);
            System.Console.WriteLine(result.Description);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task ShouldReturnNullForInvalidPokemon()
        {
            var result = await pokemonService.GetPokemon(Constants.InvalidPokemon);

            Assert.IsNull(result);
        }

        [Test]
        public async Task ShouldReturnAPokemon()
        {
            var result = await pokemonService.GetPokemon(Constants.ValidPokemon);
            System.Console.WriteLine(result.Description);

            Assert.IsInstanceOf<PokemonInfo>(result);
        }
    }
}