using AutoMapper;
using CleanArchitectureSample.Application.Dtos.Request;
using CleanArchitectureSample.Application.Dtos.Response;
using CleanArchitectureSample.Core.Aggregates;

namespace CleanArchitectureSample.Application.Mappers;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<CountryEntity, CountryResponse>().ReverseMap();
        CreateMap<CreateOrUpdateCountryRequest, CountryEntity>();
    }
}
