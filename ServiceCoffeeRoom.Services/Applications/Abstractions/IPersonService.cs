using ServiceCoffeeRoom.Services.Applications.DtoModel.Person;
using ServiceСoffeeRoom.Applications.DtoModel.Person;

namespace ServiceСoffeeRoom.Applications.Abstractions
{
    public interface IPersonService
    {
        Task<PersonDto?> GetPersonByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<PersonDto>> GetAllPersonAsync(CancellationToken cancellationToken = default);
        Task<bool> DeletePersonAsync(long id, CancellationToken cancellationToken = default);
        Task<PersonDto> CreatePersonAsync(CreatePersonDto personInfo, CancellationToken cancellationToken = default);
        Task<bool> UpdatePersonAsync(UpdatePersonDto personInfo, CancellationToken cancellationToken = default);
        Task<PersonDto> AddUserAsync(long id, CancellationToken cancellationToken = default);

    }
}
