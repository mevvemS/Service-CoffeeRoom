using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Services.Applications.Abstractions;
using ServiceCoffeeRoom.Services.Applications.DtoModel.Beans;
using ServiceCoffeeRoom.Services.Applications.Exeptions;
using ServiceСoffeeRoom.Domain;

namespace ServiceCoffeeRoom.Services.Applications
{
    public class BeansService(IBeansRepository beansRepository,
        ICoffeeMachineRepository coffeeMachineRepository) : IBeansService
    {
        public async Task<bool> AddBeansAsync(CreateBeansDto beansInfo, CancellationToken cancellationToken = default)
        {
            Beans beans = beansInfo.Weight switch
            {
                <= 0 => new(beansInfo.Mark, beansInfo.Price),
                > 0 => new(beansInfo.Mark, beansInfo.Price, beansInfo.Weight)
            };
            var result = await beansRepository.AddAsync(beans, cancellationToken);

            CoffeeMachine coffeeMachine = (await coffeeMachineRepository.GetAllAsync(cancellationToken)).FirstOrDefault()
                ?? throw new EntiyNotFoundExeption("Only", nameof(CoffeeMachine));
            if (coffeeMachine.BeansId.Equals(Guid.Empty))
                coffeeMachine.SetBeans(beans);
            else
            {
                var currentBeans = await beansRepository.GetByIdAsync(coffeeMachine.BeansId, cancellationToken);
                if (currentBeans is null || currentBeans.Status is false)
                    coffeeMachine.SetBeans(beans);
            }
            _ = await coffeeMachineRepository.UpdateAsync(coffeeMachine.Prototype(), cancellationToken);

            if (result is not null)
                return true;
            else return false;
        }


        public async Task<bool> DeleteBeansAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var beans = await beansRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new EntiyNotFoundExeption(id.ToString(), nameof(Beans));
            await beansRepository.DeleteAsync(beans, cancellationToken);
            return true;
        }
    }
}
