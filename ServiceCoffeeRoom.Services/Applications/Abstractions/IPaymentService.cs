namespace ServiceCoffeeRoom.Services.Applications.Abstractions
{
    public interface IPaymentService
    {
        Task<bool> AddCashAsync(long id, int cash, CancellationToken cancellationToken = default);
    }
}
