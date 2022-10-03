using AutoMapper;
using Models;
using ModelsDb;

namespace Services;

public class MapperService
{
    
    public Mapper MapperFromClientDbInClient { get; private init; }
    public Mapper MapperFromClientInClientDb{ get; private init;}
    public Mapper MapperFromAccountInAccountDb{ get; private init;}
    public Mapper MapperFromEmployeeInEmployeeDb{ get; private init;}
    public Mapper MapperFromEmployeeDbInEmployee{ get; private init;}
    
    public MapperService()
    {
        MapperFromClientDbInClient = new Mapper(new MapperConfiguration(cfg =>
            cfg.CreateMap<ClientDb, Client>()));
        MapperFromClientInClientDb = new Mapper(ConfigurateClientInClientDb());
        MapperFromAccountInAccountDb = new Mapper(ConfigurateAccountInAccountDb());
        MapperFromEmployeeInEmployeeDb = new Mapper(ConfigurateEmployeeInEmployeeDb());
        MapperFromEmployeeDbInEmployee = new Mapper(ConfigurateEmployeeDbInEmployee());

    }

    private IConfigurationProvider ConfigurateClientInClientDb()
    {
        return new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDb>()
            .ForMember(desc => desc.AccountsDbs, src => new List<AccountDb>()));
    }
    private IConfigurationProvider ConfigurateAccountInAccountDb()
    {
        return new MapperConfiguration(cfg => cfg.CreateMap<Account, AccountDb>()
            .ForMember(desc => desc.ClientDb, src => 
                new TestDataGenerator().GeneratingClient())
            .ForMember(desc => desc.CurrencyDb, src => 
                new TestDataGenerator().GeneratingCurrency()));
    }
    private IConfigurationProvider ConfigurateEmployeeInEmployeeDb()
    {
        return new MapperConfiguration(cfg => 
            cfg.CreateMap<Employee, EmployeeDb>());
    }
    private IConfigurationProvider ConfigurateEmployeeDbInEmployee()
    {
        return new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDb, Employee>()
            .ForMember(desc => desc.Id, src => src.Ignore()));
    }
    
}