using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using ServiceContracts.Enums;

namespace CRUDTests

{
    public class PersonServiceTest
    {
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;

        public PersonServiceTest()
        {
            _personsService = new PersonsService();
            _countriesService = new CountriesService();
        }

        #region AddPerson

        //When we supply null value as person add request, it should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest? personAddRequest = null;

            //Act
            Assert.Throws<ArgumentNullException>(() => 
            {
                _personsService.AddPerson(personAddRequest);
            });
        }


        //When we supply null value as personName, it should throw argument exception
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { PersonName = null};

            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.AddPerson(personAddRequest);
            });
        }

        //When we upply proper person details, it should insert the person into persons list; and it should return an object of personResponse, which includes the newly generated person id
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { PersonName = "Person1", Email="person1@example.com", Address="sample address", CountryId = Guid.NewGuid(),
            Gender = GenderOptions.Male, DateOfBirth=DateTime.Parse("2000-01-01")};

            //Act
            PersonResponse person_Response_From_Add = _personsService.AddPerson(personAddRequest);
            List<PersonResponse> persons_list = _personsService.GetAllPersons();

            //Assert
            Assert.True(person_Response_From_Add.PersonId != Guid.Empty);
            Assert.Contains(person_Response_From_Add, persons_list);
        }
        #endregion

        #region GetPersonByPersonId

        //If we supply null as PersonID, it should return null as PersonResponse
        [Fact]
        public void GetPersonByPersonId_NullPersonId()
        {
            //Arrange
            Guid? PersonId = null;

            //Act
            PersonResponse? person_response_from_get = _personsService.GetPersonByPersonId(PersonId);

            //Assert
            Assert.Null(person_response_from_get);
        }

        //If we supply a valid person id, it should return the valid person details as PersonResponse object
        [Fact]
        public void GerPersonByPersonId_WithPersonId()
        {
            //Arrange
            CountryAddRequest? country_request = new CountryAddRequest() { CountryName = "Canada" };
            CountryResponse country_response = _countriesService.AddCountry(country_request);

            //Act
            PersonAddRequest person_request = new PersonAddRequest()
            {
                PersonName = "ABC",
                Email = "person@person.com",
                Address = "address",
                CountryId = country_response.CountryId,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false
            };

            PersonResponse person_response_from_add = _personsService.AddPerson(person_request);
           
            PersonResponse? person_response_from_get = _personsService.GetPersonByPersonId(person_response_from_add.PersonId);

            //Assert
            Assert.Equal(person_response_from_add, person_response_from_get);
        }
        #endregion
    }
}
