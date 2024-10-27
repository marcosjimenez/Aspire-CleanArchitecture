using AutoMapper;
using CleanArchitectureSample.Application.Dtos.Request;
using CleanArchitectureSample.Application.Dtos.Response;
using CleanArchitectureSample.Core.Aggregates;

namespace CleanArchitectureSample.Application.Mappers;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<ContactEntity, ContactResponse>().ReverseMap();
        CreateMap<CreateOrUpdateContactRequest, ContactEntity>();
    }
}
