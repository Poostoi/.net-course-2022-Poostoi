using AutoMapper;
using Models;
using ModelsDb;

namespace Services;

public class MapperService
{
    
    public Mapper MapperFromClientDbInClient { get; private init; }
    public Mapper MapperFromClientInClientDb{ get; private init;}
    public MapperService()
    {
        MapperFromClientDbInClient = new Mapper(new MapperConfiguration(cfg =>
            cfg.CreateMap<ClientDb, Client>()));
        MapperFromClientInClientDb = new Mapper(ConfigurateClientInClientDb());
    }

    private IConfigurationProvider ConfigurateClientInClientDb()
    {
        return new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDb>()
            .ForMember(desc => desc.AccountsDbs, src => new List<AccountDb>())
            .ForMember(desc => desc.Id, src => src.Ignore()));
    }
    
}