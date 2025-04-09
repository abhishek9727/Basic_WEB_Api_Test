using AutoMapper;
using Data_Layer.Model;
using Web_Api_Core_.DTO;
using Web_Api_Core_.Model;

namespace Web_Api_Core_.Helper
{
    public class MyMappingProfile: Profile
    {
        public MyMappingProfile()
        {
            CreateMap<Pokemon, PokemonVM>();
            CreateMap<Category, CategoryVM>();
            CreateMap<Country, CountryVM>();
            CreateMap<Owner, OwnrVM>();
            CreateMap<Review, ReviewVM>();
            CreateMap<Reviewer, ReviewerVM>();
        }
    }
}
