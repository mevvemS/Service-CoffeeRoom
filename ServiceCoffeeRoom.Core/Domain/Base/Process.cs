using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;

namespace ServiceСoffeeRoom.Domain.Base
{
    public abstract class Process : Entity<Guid>
    {
        [Column]
        public long VisitorId { get; protected set; }
        [Column]
        public Guid RoomId { get; protected set; }
        [Column]
        public DateTime TimeHappened { get; protected set; }
        public Process(Guid id, Room room, Person person) : base(id)
        {
            RoomId = room.Id;
            VisitorId = person.Id;
            TimeHappened = DateTime.Now.ToUniversalTime();
        }
        protected Process(Guid id) : base(id)
        {
        }
    }
}
