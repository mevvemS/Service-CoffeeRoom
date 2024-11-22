namespace ServiceCoffeeRoom.Services.Applications.Abstractions
{
    public interface IServiceService
    {
        Task<bool> AddServiceAsync(long id, CancellationToken cancellationToken = default);
    }
}
