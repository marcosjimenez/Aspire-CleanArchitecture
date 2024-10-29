using AutoMapper;
using CleanArchitectureSample.Application.Dto.Request;
using CleanArchitectureSample.Application.Dto.Response;
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
