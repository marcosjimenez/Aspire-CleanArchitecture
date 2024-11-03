using AutoMapper;
using CleanArchitectureSample.Application.Dto.Request;
using CleanArchitectureSample.Application.Dto.Response;
using CleanArchitectureSample.Aspire.Web.Model;

namespace CleanArchitectureSample.Aspire.Web.Mappers;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<ContactResponse, ContactModel>().ReverseMap();
        CreateMap<ContactModel, CreateOrUpdateContactRequest>();
    }
}
