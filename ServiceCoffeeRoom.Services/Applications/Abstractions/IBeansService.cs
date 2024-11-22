using ServiceCoffeeRoom.Services.Applications.DtoModel.Beans;
using ServiceCoffeeRoom.Services.Applications.DtoModel.Person;
using ServiceСoffeeRoom.Applications.DtoModel.Person;
using ServiceСoffeeRoom.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCoffeeRoom.Services.Applications.Abstractions
{
    public interface IBeansService
    {
        Task<bool> DeleteBeansAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> AddBeansAsync(CreateBeansDto beansInfo, CancellationToken cancellationToken = default);
    }
}
