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
        private readonly IShakespeareTranslationService _shakespeareTranslationService;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(IPokemonService pokemonService, IShakespeareTranslationService shakespeareTranslationService, ILogger<PokemonController> logger)
        {
            _pokemonService = pokemonService;
            _shakespeareTranslationService = shakespeareTranslationService;
            _logger = logger;
        }

        [HttpGet]
        [Route("{name}")]
        public IActionResult Get(string name)
        {
            var result = _pokemonService.GetPokemon(name);
            if (result.Result == null) return NotFound($"{name} not found");

            result.Result.Description = _shakespeareTranslationService.Translate(result.Result.Description).Result;
            return Ok(result.Result);
        }
    }
}