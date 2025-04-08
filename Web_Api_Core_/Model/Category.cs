using Web_Api_Core_.Model.Joins_Tables;

namespace Web_Api_Core_.Model
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection< PokemonCategory> PokemonCategories { get; set; }
    }
}
