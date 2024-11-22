using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Services.Applications.Abstractions;
using ServiceCoffeeRoom.Services.Applications.Exeptions;
using ServiceСoffeeRoom.Domain;

namespace ServiceCoffeeRoom.Services.Applications
{
    public class ServiceService(IServiceRepository serviceRepository,
        IRoomRepository roomRepository,
        IPersonRepository personRepository,
        ICoffeeMachineRepository coffeeMachineRepository) : IServiceService
    {
        public async Task<bool> AddServiceAsync(long id, CancellationToken cancellationToken = default)
        {
            Person? person = await personRepository.GetByIdAsync(id, cancellationToken);
            if (person is null) return false;

            Room? room = (await roomRepository.GetAllAsync(cancellationToken)).FirstOrDefault();
            if (room is null) return false;

            CoffeeMachine machine = await coffeeMachineRepository.GetByIdAsync(room.CoffeeMachineId, cancellationToken)
                ?? throw new EntiyNotFoundExeption(room.CoffeeMachineId.ToString(), nameof(CoffeeMachine));

            if (!machine.AddService()) 
                throw new ServicePeriodCoffeeMachineExeption(room.CoffeeMachineId.ToString(), nameof(CoffeeMachine));
            var cash = room.PriceService;
            var serviceEntity = new ServiceСoffeeRoom.Domain.Processes.Service(room, person, cash);
            _ = await serviceRepository.AddAsync(serviceEntity, cancellationToken);
            _ = await roomRepository.UpdateAsync(room.Prototype(), cancellationToken);
            _ = await coffeeMachineRepository.UpdateAsync(machine.Prototype(), cancellationToken);
            return await personRepository.UpdateAsync(person.Prototype(), cancellationToken);           
        }
    }
}
