using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCoffeeRoom.Services.Applications.Abstractions
{
    public interface ICupService
    {
        Task<bool> UseCoffeeMachineAsync(long id, CancellationToken cancellationToken = default);
    }
}
