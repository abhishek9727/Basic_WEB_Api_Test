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
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _owner;
        private readonly IMapper _mapper;
        public OwnerController(IOwnerRepository owner, IMapper mapper)
        {
            _owner = owner;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public ActionResult GetOwners()
        {
            var owner = _mapper.Map<List<OwnrVM>>(_owner.GetOwners());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public ActionResult GetOwner(int ownerId)
        {
            if (!_owner.OwnerExists(ownerId))
                return NotFound();

            var owner = _mapper.Map<OwnrVM>(_owner.GetOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);

        }

        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public ActionResult GetPokemonByOwner(int ownerId)
        {
            if(!_owner.OwnerExists(ownerId))
            {
                return NotFound();
            }
            var owner = _mapper.Map<List<PokemonVM>>(_owner.GetPokemonByOwner(ownerId));    
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(owner);
        }



    }
}
