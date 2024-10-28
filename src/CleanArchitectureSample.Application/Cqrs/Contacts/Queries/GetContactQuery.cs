using CleanArchitectureSample.Application.Dto.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Queries;

public class GetContactQuery(int id) : IRequest<ContactResponse>
{
    public int Id { get; } = id;
}
