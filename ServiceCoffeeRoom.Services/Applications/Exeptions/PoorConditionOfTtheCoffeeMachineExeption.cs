namespace ServiceCoffeeRoom.Services.Applications.Exeptions
{
    public class PoorConditionOfTtheCoffeeMachineExeption(string id, string nameOfEntity)
        : Exception($"The {nameOfEntity} with Id {id} has not prepared.");
}
