using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokeSpeare.Service.Services;

namespace PokeSpeare.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(IPokemonService pokemonService, ILogger<PokemonController> logger)
        {
            _pokemonService = pokemonService;
            _logger = logger;
        }

        [HttpGet]
        [Route("{name}")]
        public IActionResult Get(string name)
        {
            var result = _pokemonService.GetPokemon(name);
            if (result.Result == null) return NotFound($"{name} not found");

            return Ok(result.Result);
        }
    }
}