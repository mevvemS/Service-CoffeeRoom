using ServiceСoffeeRoom.Domain;

namespace ServiceCoffeeRoom.Services.Applications.Exeptions
{
    public class AnotherPersonException(Person person) : InvalidOperationException("")
    {
        public Person Person => person;
    }
}
