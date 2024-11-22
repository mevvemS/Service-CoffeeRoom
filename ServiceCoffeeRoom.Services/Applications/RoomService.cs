using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Services.Applications.DtoModel.Beans;
using ServiceCoffeeRoom.Services.Applications.DtoModel.CoffeeMachineModel;
using ServiceCoffeeRoom.Services.Applications.DtoModel.Person;
using ServiceCoffeeRoom.Services.Applications.DtoModel.Room;
using ServiceCoffeeRoom.Services.Applications.Exeptions;
using ServiceСoffeeRoom.Applications.Abstractions;
using ServiceСoffeeRoom.Domain;

namespace ServiceСoffeeRoom.Applications
{
    public class RoomService(IRoomRepository roomRepository,
                             ICoffeeMachineRepository coffeeMachineRepository,
                             IPersonRepository personRepository,
                             IBeansRepository beansRepository) : IRoomService
    {
        public async Task<RoomDto?> GetRoom(CancellationToken token = default)
        {
            Room room = (await roomRepository.GetAllAsync(token)).FirstOrDefault();
            if (room is null) return null;

            CoffeeMachine machine = await coffeeMachineRepository.GetByIdAsync(room.CoffeeMachineId, token)
                ?? throw new EntiyNotFoundExeption(room.CoffeeMachineId.ToString(), nameof(CoffeeMachine));

            Person admin = await personRepository.GetByIdAsync(room.AdminId, token)
                ?? throw new EntiyNotFoundExeption(room.AdminId.ToString(), nameof(Person));
            List<PersonDto> users = (await personRepository.GetAllAsync()).Where(x => x.IsUser)
                .Select(person => new PersonDto()
                {
                    Id = person.Id,
                    Name = person.Name,
                    TelegramAccaunt = person.TelegramAccaunt,
                    IsAdmin = person.IsAdmin,
                    IsUser = person.IsUser,
                    CashAccount = person.CashAccount
                }).ToList();

            var currentBeans = machine.BeansId.Equals(Guid.Empty) switch
            {
                true => null,
                false => await beansRepository.GetByIdAsync(machine.BeansId, token)            
            };

            var beansDto = (currentBeans is not null) switch
            {
                true => new BeansDto() 
                { 
                    Id = currentBeans.Id, 
                    Mark = currentBeans.Mark,
                    Price = currentBeans.Price,
                    RemainingWeight = currentBeans.RemainingWeight,
                    Status = currentBeans.Status,
                    Weight = currentBeans.Weight
                },
                false => null
            };
                

            var coffeeMachine = new CoffeeMachineDto()
            {
                Name = machine.Name,
                Beans = beansDto,
                PriceСup = room.GetCurrentPriceCoffeeCup(machine, currentBeans!),
                CountCupAll = machine.CountCupAll,
                CountCupService = machine.CountCupService,
                LimitService = machine.LimitService,
                SizeOfOneCup = machine.SizeOfOneCup,
            };

            return new RoomDto()
            {
                Id = room.Id,
                Name = room.Name,
                Admin = new AdminDto() { Id = admin.Id, Name = admin.Name, TelegramAccaunt = admin.TelegramAccaunt },
                CoffeeMachine = coffeeMachine,
                PriceService = room.PriceService,
                Bank = room.Bank,
                Users = users
            };
        }

        public async Task<RoomDto> CreateRoomAsync(CreateRoomDto roomInfo, CancellationToken token = default)
        {
            Person admin = await personRepository.GetByIdAsync(roomInfo.AdminId)
                ?? throw new EntiyNotFoundExeption(roomInfo.AdminId.ToString(), nameof(Person));

            Room room = new Room(roomInfo.AdminId, roomInfo.Name);
            CoffeeMachine machine = new CoffeeMachine(roomInfo.Name);
            Beans beans = new Beans(mark: "default", 0, 0) { Status = false};
            _ = await beansRepository.AddAsync(beans, token);
            machine.SetBeans(beans);

            room.SetCoffemachine(machine.Id);
            _ = await coffeeMachineRepository.AddAsync(machine);

            Room result = await roomRepository.AddAsync(room, token);

            List<PersonDto> users = (await personRepository.GetAllAsync()).Where(x => x.IsUser)
                .Select(person => new PersonDto()
                {
                    Id = person.Id,
                    Name = person.Name,
                    TelegramAccaunt = person.TelegramAccaunt,
                    IsAdmin = person.IsAdmin,
                    IsUser = person.IsUser,
                    CashAccount = person.CashAccount
                }).ToList();

            admin.SetIsAdmin(true);
            admin.SetIsUser(true);
            _ = await personRepository.UpdateAsync(admin.Prototype(), token);
           
            var coffeeMachine = new CoffeeMachineDto()
            {
                Name = machine.Name,
                CountCupAll = machine.CountCupAll,
                CountCupService = machine.CountCupService,
                LimitService = machine.LimitService,
                SizeOfOneCup = machine.SizeOfOneCup,
            };

            return new RoomDto()
            {
                Id = result.Id,
                Name = result.Name,
                Admin = new AdminDto() { Id = admin.Id, Name = admin.Name, TelegramAccaunt = admin.TelegramAccaunt },
                CoffeeMachine = coffeeMachine,
                PriceService = result.PriceService,
                Bank = result.Bank,
                Users = users
            };
        }

        public async Task<bool> DeleteRoom(Guid id, CancellationToken token = default)
        {
            Room room = await roomRepository.GetByIdAsync(id, token)
                ?? throw new EntiyNotFoundExeption(id.ToString(), nameof(Room));
            _ = await coffeeMachineRepository.DeleteAsync(room.CoffeeMachineId, token);
            Person admin = await personRepository.GetByIdAsync(room.AdminId, token)
                ?? throw new EntiyNotFoundExeption(room.AdminId.ToString(), nameof(Person));
            admin.SetIsAdmin(false);
            _ = await personRepository.UpdateAsync(admin.Prototype(), token);
            return await roomRepository.DeleteAsync(id, token);
        }

        public async Task<bool> UpdateRoom(UpdateRoomDto roomInfo, CancellationToken token = default)
        {
            Room room = await roomRepository.GetByIdAsync(roomInfo.Id, token)
               ?? throw new EntiyNotFoundExeption(roomInfo.Id.ToString(), nameof(Room));
            if (room.AdminId.Equals(roomInfo.AdminId) is false)
            {
                Person oldAdmin = await personRepository.GetByIdAsync(room.AdminId, token)
                     ?? throw new EntiyNotFoundExeption(room.AdminId.ToString(), nameof(Person));
                oldAdmin.SetIsAdmin(false);
                _ = await personRepository.UpdateAsync(oldAdmin.Prototype(), token);

                Person newAdmin = await personRepository.GetByIdAsync(roomInfo.AdminId, token)
                     ?? throw new EntiyNotFoundExeption(roomInfo.AdminId.ToString(), nameof(Person));
                newAdmin.SetIsAdmin(true);
                _ = await personRepository.UpdateAsync(oldAdmin.Prototype(), token);
            }
            room.SetName(roomInfo.Name);
            room.SetPriceService(roomInfo.PriceService);
            CoffeeMachine coffeemachine = await coffeeMachineRepository.GetByIdAsync(room.CoffeeMachineId)
               ?? throw new EntiyNotFoundExeption(room.CoffeeMachineId.ToString(), nameof(CoffeeMachine));
            coffeemachine.SetName(roomInfo.Name);
            coffeemachine.SetProperty(roomInfo.LimitService);
            _ = await coffeeMachineRepository.UpdateAsync(coffeemachine.Prototype(), token);
            return await roomRepository.UpdateAsync(room.Prototype(), token);
        }
    }
}
