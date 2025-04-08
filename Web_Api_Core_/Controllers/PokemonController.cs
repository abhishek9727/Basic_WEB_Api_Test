using AutoMapper;
using Businnes_Layer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api_Core_.DTO;
using Web_Api_Core_.Model;

namespace Web_Api_Core_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepo;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepo, IMapper mapper)
        {
            _pokemonRepo = pokemonRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonVM>>(_pokemonRepo.GetPokemons());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId)
        {
            if(!_pokemonRepo.PokemonExists(pokeId))
                return NotFound();

            var pokemon = _mapper.Map<PokemonVM>(_pokemonRepo.GetPokemon(pokeId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);

        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if(!_pokemonRepo.PokemonExists(pokeId))
                return NotFound();

            var rating = _pokemonRepo.GetPokemonRating(pokeId);
            if(!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
            
        }



    }
}
