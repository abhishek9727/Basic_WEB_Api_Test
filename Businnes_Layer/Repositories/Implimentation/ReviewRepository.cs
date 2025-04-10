using Businnes_Layer.Repositories.Interfaces;
using Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Api_Core_.Model;

namespace Businnes_Layer.Repositories.Implimentation
{
    public class ReviewRepository : IReviewRepository
    {
         private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public Review GetReview(int reviewid)
        {
            return _context.Reviews.Where(c => c.id == reviewid).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public ICollection<Review> GetreviewsOfPokemon(int pokeId)
        {
            return _context.Reviews.Where(c => c.Pokemon.Id == pokeId).ToList();
        }

        public bool ReviewExists(int reviewid)
        {
            return _context.Reviews.Any(r => r.id == reviewid);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
