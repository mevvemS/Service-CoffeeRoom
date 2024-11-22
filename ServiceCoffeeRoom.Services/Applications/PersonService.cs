using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Services.Applications.DtoModel.Person;
using ServiceCoffeeRoom.Services.Applications.Exeptions;
using ServiceСoffeeRoom.Applications.Abstractions;
using ServiceСoffeeRoom.Applications.DtoModel.Person;
using ServiceСoffeeRoom.Domain;

namespace ServiceСoffeeRoom.Applications
{
    public class PersonService(IPersonRepository personRepository,
                               ICupRepository cupRepository) : IPersonService
    {
        public async Task<PersonDto> AddUserAsync(long id, CancellationToken cancellationToken = default)
        {
            Person? person = await personRepository.GetByIdAsync(id, cancellationToken);
            if (person is not null)
            {
                person.SetIsUser(true);
                await personRepository.UpdateAsync(person.Prototype(),token: cancellationToken);            
            }
            else
            {
                person = new Person(id);
                person.SetIsUser(true);
                person = await personRepository.AddAsync(person, cancellationToken);
            }

            return new PersonDto()
            {
                Id = person.Id,
                Name = person.Name,
                TelegramAccaunt = person.TelegramAccaunt,
                IsAdmin = person.IsAdmin,
                IsUser = person.IsUser,
                CashAccount = person.CashAccount
            };
        }

        public async Task<PersonDto> CreatePersonAsync(CreatePersonDto personInfo, CancellationToken cancellationToken = default)
        {
            var person = new Person(personInfo.Id, personInfo.Name, personInfo.TelegramAccaunt, false, false);
            Person result = await personRepository.AddAsync(person, cancellationToken);
            return new PersonDto()
            {
                Id = result.Id,
                Name = result.Name,
                TelegramAccaunt = result.TelegramAccaunt,
                IsAdmin = result.IsAdmin,
                IsUser = result.IsUser,
                CashAccount = result.CashAccount
            };
        }

        public async Task<bool> DeletePersonAsync(long id, CancellationToken cancellationToken = default)
        {
            return await personRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<PersonDto>> GetAllPersonAsync(CancellationToken cancellationToken = default)
        {
            var persons = await personRepository.GetAllAsync(cancellationToken);
            return persons.Select(x => new PersonDto()
            {
                Id = x.Id,
                Name = x.Name,
                TelegramAccaunt = x.TelegramAccaunt
            });
        }

        public async Task<PersonDto?> GetPersonByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            Person? person = await personRepository.GetByIdAsync(id, cancellationToken);
            if (person is null)
                return null;

            return new PersonDto()
            {
                Id = person.Id,
                Name = person.Name,
                TelegramAccaunt = person.TelegramAccaunt,
                IsAdmin = person.IsAdmin,
                IsUser = person.IsUser,
                CashAccount = person.CashAccount
            };
        }

        public async Task<bool> UpdatePersonAsync(UpdatePersonDto personInfo, CancellationToken cancellationToken = default)
        {
            Person person = await personRepository.GetByIdAsync(personInfo.Id, cancellationToken)
                ?? throw new EntiyNotFoundExeption(personInfo.Id.ToString(), nameof(Person));
            person.SetName(personInfo.Name);
            person.SetTelegramAccaunt(personInfo.TelegramAccaunt);
            return await personRepository.UpdateAsync(person.Prototype(), cancellationToken);
        }
    }
}
