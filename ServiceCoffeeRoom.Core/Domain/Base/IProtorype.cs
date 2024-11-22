namespace ServiceCoffeeRoom.Core.Domain.Base
{
    public interface IProtorype<TEntity>
    {
        TEntity Prototype();
    }
}
