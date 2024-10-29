using CleanArchitectureSample.Application.Dto.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Queries;

public class GetAllContactsQuery : IRequest<IEnumerable<ContactResponse>>
{
}
