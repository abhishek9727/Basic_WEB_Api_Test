using AutoMapper;
using Web_Api_Core_.DTO;
using Web_Api_Core_.Model;

namespace Web_Api_Core_.Helper
{
    public class MyMappingProfile: Profile
    {
        public MyMappingProfile()
        {
            CreateMap<Pokemon, PokemonVM>();
        }
    }
}
