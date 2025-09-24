using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating country entity
    /// </summary>
    public interface ICountriesService
    {

        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest"></param>
        /// <returns>Returns the country object after adding it (including the newly generated country id)</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        List<CountryResponse> GetAllCountries();

        /// <summary>
        /// Returns a country object based on the given country id
        /// </summary>
        /// <param name="countryId">CountryId (guid) to search</param>
        /// <returns>Matching Country as CountryResponse Object</returns>
        CountryResponse? GetCountryById(Guid? countryId);
    }
}
