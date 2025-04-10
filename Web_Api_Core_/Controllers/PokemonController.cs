using AutoMapper;
using Businnes_Layer.Repositories.Implimentation;
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
        public ActionResult GetPokemons()
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
        public ActionResult GetPokemon(int pokeId)
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
        public ActionResult GetPokemonRating(int pokeId)
        {
            if(!_pokemonRepo.PokemonExists(pokeId))
                return NotFound();

            var rating = _pokemonRepo.GetPokemonRating(pokeId);
            if(!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
            
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult CreateOwner([FromQuery] int ownerId,[FromQuery] int categoryId, [FromBody] PokemonVM pokeCreate)
        {
            if (pokeCreate == null)
            {
                return BadRequest(ModelState);
            }
            var pokemon = _pokemonRepo.GetPokemons()
                .Where(c => c.Name.Trim().ToUpper() == pokeCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (pokemon != null)
            {
                ModelState.AddModelError("", " Pokemon Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pokemonMap = _mapper.Map<Pokemon>(pokeCreate);

            if (!_pokemonRepo.CreatePokemon(ownerId, categoryId, pokemonMap))
            {
                ModelState.AddModelError("", "Something Went Wrong While Saving");
                return StatusCode(500, ModelState);
            }

            return Ok("SucessFully Created");


        }



    }
}
