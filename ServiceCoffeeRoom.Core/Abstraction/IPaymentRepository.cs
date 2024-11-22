using ServiceСoffeeRoom.Domain.Processes;

namespace ServiceCoffeeRoom.Core.Abstraction
{
    public  interface IPaymentRepository: IRepository<Payment, Guid>
    {
    }
}
