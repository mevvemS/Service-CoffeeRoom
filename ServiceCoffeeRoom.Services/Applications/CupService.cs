using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Services.Applications.Abstractions;
using ServiceCoffeeRoom.Services.Applications.Exeptions;
using ServiceСoffeeRoom.Domain;
using ServiceСoffeeRoom.Domain.Processes;

namespace ServiceCoffeeRoom.Services.Applications
{
    public class CupService(IRoomRepository roomRepository,
                            ICoffeeMachineRepository coffeeMachineRepository,
                            IPersonRepository personRepository,
                            IBeansRepository beansRepository,
                            ICupRepository cupRepository) :ICupService
    {
        public async Task<bool> UseCoffeeMachineAsync(long id, CancellationToken cancellationToken = default)
        {
            Person? person = await personRepository.GetByIdAsync(id, cancellationToken);
            if (person is null) return false;

            Room? room = (await roomRepository.GetAllAsync(cancellationToken)).FirstOrDefault();
            if (room is null) return false;

            CoffeeMachine machine = await coffeeMachineRepository.GetByIdAsync(room.CoffeeMachineId, cancellationToken)
                ?? throw new EntiyNotFoundExeption(room.CoffeeMachineId.ToString(), nameof(CoffeeMachine));
            Beans? beans = await beansRepository.GetByIdAsync(machine.BeansId, cancellationToken);
                if (beans is null) return false;
           
            if (!machine.UseCup())
                throw new PoorConditionOfTtheCoffeeMachineExeption(machine.Id.ToString(), nameof(CoffeeMachine));
            if (!beans.Use(machine.SizeOfOneCup))
                return false;
            var line = new CupOfCoffe(room, room.GetCurrentPriceCoffeeCup(machine, beans), person);
            _ = await cupRepository.AddAsync(line, cancellationToken);
            _ = await beansRepository.UpdateAsync(beans.Prototype(), cancellationToken);
            _ = await coffeeMachineRepository.UpdateAsync(machine.Prototype(), cancellationToken);
            _ = await roomRepository.UpdateAsync(room.Prototype(), cancellationToken);
            _ = await personRepository.UpdateAsync(person.Prototype(), cancellationToken);

            return true;
        }
    }
}
