namespace ServiceCoffeeRoom.Services.Applications.Exeptions
{
    public class EntiyNotFoundExeption(string id, string nameOfEntity)
        : Exception($"The {nameOfEntity} with Id {id} has not been found.");
}
