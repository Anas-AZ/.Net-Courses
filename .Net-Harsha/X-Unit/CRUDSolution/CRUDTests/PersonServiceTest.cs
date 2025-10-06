using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using ServiceContracts.Enums;

namespace CRUDTests

{
    public class PersonServiceTest
    {
        private readonly IPersonsService _personsService;

        public PersonServiceTest()
        {
            _personsService = new PersonsService();
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
    }
}
