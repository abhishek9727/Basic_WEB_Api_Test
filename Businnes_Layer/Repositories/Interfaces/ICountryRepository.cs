using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Api_Core_.Model;

namespace Businnes_Layer.Repositories.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetAllCountries();

        Country GetCountryById(int id);

        Country GetCountryByOwner(int  nameId);  

        ICollection<Owner> GetOwnersFromCountry(int countryId);

        bool countryExists(int id);


    }
}
