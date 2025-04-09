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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        private readonly IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public ActionResult GetReviews()
        {
            var review = _mapper.Map<List<ReviewVM>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public ActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var review = _mapper.Map<PokemonVM>(_reviewRepository.GetReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);

        }


        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public ActionResult GetReviewForPokemon(int pokeId)
        {
            var review = _mapper.Map<List<ReviewVM>>
                 (_reviewRepository.GetreviewsOfPokemon(pokeId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);

        }

    }
}
