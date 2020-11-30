using System.Threading.Tasks;
using PokeSpeare.Service.Models;

namespace PokeSpeare.Service.Services
{
    public interface IPokemonService
    {
        Task<PokemonInfo> GetPokemon(string name);
    }
}