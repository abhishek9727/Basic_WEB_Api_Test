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
    public class ReviewersController : ControllerBase
    {
        private readonly IReviewersRepository _reviewersRepository;
        private readonly IMapper _mapper;

        public ReviewersController(IReviewersRepository reviewersRepository, IMapper mapper)
        {
            _reviewersRepository = reviewersRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public ActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerVM>>(_reviewersRepository.GetReviewers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public ActionResult GetReviewer(int reviewerId)
        {
            if (!_reviewersRepository.ReveiewerExists(reviewerId))
                return NotFound();

            var reviewr = _mapper.Map<ReviewerVM>(_reviewersRepository.GetReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewr);

        }

        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public ActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewersRepository.ReveiewerExists(reviewerId))
                return NotFound();

            var reviews = _mapper.Map<List<ReviewVM>>
                (_reviewersRepository.GetReviewesByReviewers(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);

        }




    }
}
