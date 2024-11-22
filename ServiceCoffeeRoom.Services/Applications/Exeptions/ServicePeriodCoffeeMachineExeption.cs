namespace ServiceCoffeeRoom.Services.Applications.Exeptions
{
    public class ServicePeriodCoffeeMachineExeption(string id, string nameOfEntity)
        : Exception($"The {nameOfEntity} with Id {id} service period has not yet.");
}
