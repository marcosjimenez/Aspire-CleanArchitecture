using AutoMapper;
using CleanArchitectureSample.Application.Dto.Request;
using CleanArchitectureSample.Application.Dto.Response;
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
