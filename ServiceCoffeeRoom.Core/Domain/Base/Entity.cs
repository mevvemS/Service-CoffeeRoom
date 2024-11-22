using LinqToDB.Mapping;

namespace ServiceСoffeeRoom.Domain.Base
{
    public abstract class Entity<TId>(TId id)
        where TId : struct
    {
        [PrimaryKey]
        public TId Id { get; protected set; } = id;
    }
}
