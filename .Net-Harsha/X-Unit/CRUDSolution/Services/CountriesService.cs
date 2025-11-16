using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        //private field
        private readonly List<Country> _countries;

        public CountriesService(bool initialize = true)
        {
            _countries = new List<Country>();

            if (initialize) { 
                _countries.AddRange(new List<Country>() {
                new Country() { CountryId = Guid.Parse("E0976648-69EE-421E-A1BD-B8081F668B53"), CountryName = "USA" },
                                        
                new Country() { CountryId = Guid.Parse("90E6954A-CDD4-42CA-937F-BDA5112815A0"), CountryName = "Canada" },
                                        
                new Country() { CountryId = Guid.Parse("FAF7D91F-6945-428B-80F7-1BE25E52208F"), CountryName = "UK" },
                                        
                new Country() { CountryId = Guid.Parse("85B28A23-81B8-45A1-BE9E-AD4FBBC670DC"), CountryName = "India" },
                                        
                new Country() { CountryId = Guid.Parse("E9136BF1-E8F5-4CEF-A452-3BBE7089BEC4"), CountryName = "Australia" }
                });
            }
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            //Validation: countryAddRequest parameter can't be null
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            //Validation: countryName can't be null
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }

            //Validation: countryNAme can't be duplicate
            if(_countries.Where(temp => temp.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException("Given country name already exists");
            }

            //Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            //generate CountryId
            country.CountryId = Guid.NewGuid();

            //Add country object into countries
            _countries.Add(country);

            return country.ToCountryResponse();
        }

        /// <summary>
        /// Returns all countries from the list
        /// </summary>
        /// <returns>All countries from the list as List of CountryResponse</returns>
        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryById(Guid? countryId)
        {
            if (countryId == null) 
            { 
                return null;
            }

            Country? countrty_response_from_list = _countries.FirstOrDefault(temp => temp.CountryId == countryId);

            if (countrty_response_from_list == null)
                return null;

            return countrty_response_from_list.ToCountryResponse();
        }
    }
}
