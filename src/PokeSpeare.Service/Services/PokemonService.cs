using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PokeApiNet;
using PokeSpeare.Service.Models;

namespace PokeSpeare.Service.Services
{
    public class PokemonService : IPokemonService
    {
        private PokeApiClient _pokeClient;

        public PokemonService(PokeApiClient client)
        {
            _pokeClient = client;
        }

        public async Task<PokemonInfo> GetPokemon(string name)
        {
            PokemonSpecies pokemonSpecies = null;
            try
            {
                pokemonSpecies = await _pokeClient.GetResourceAsync<PokemonSpecies>(name);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
            }

            return pokemonSpecies == null
                ? null
                : new PokemonInfo
                {
                    Name = name,
                    Description = GetRandomDescription(pokemonSpecies
                        .FlavorTextEntries
                        .Where(x => x.Language.Name == "en")
                        .ToList())

                };
        }

        private string GetRandomDescription(List<PokemonSpeciesFlavorTexts> flavorTextEntries)
        {
            var rng = new Random();
            var randomIndex = rng.Next(0, flavorTextEntries.Count - 1);

            return flavorTextEntries[randomIndex].FlavorText;
        }
    }
}