using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Services.Applications.Abstractions;
using ServiceСoffeeRoom.Domain;
using ServiceСoffeeRoom.Domain.Processes;

namespace ServiceCoffeeRoom.Services.Applications
{
    public class PaymentService(IRoomRepository roomRepository,
                                IPersonRepository personRepository,
                                IPaymentRepository paymentRepository) : IPaymentService
    {
        public async Task<bool> AddCashAsync(long id, int cash, CancellationToken cancellationToken = default)
        {
            Person? person = await personRepository.GetByIdAsync(id, cancellationToken);
            if (person is null) return false;

            Room? room = (await roomRepository.GetAllAsync(cancellationToken)).FirstOrDefault();
            if (room is null) return false;
            room.AddCashBank(-1 * cash);
            person.AddCash(cash);
            var line = new Payment(room, person, cash);
                        
            _ = await paymentRepository.AddAsync(line, cancellationToken);
            _ = await roomRepository.UpdateAsync(room.Prototype(), cancellationToken);
            _ = await personRepository.UpdateAsync(person.Prototype(), cancellationToken);

            return true;
        }
    }
}
