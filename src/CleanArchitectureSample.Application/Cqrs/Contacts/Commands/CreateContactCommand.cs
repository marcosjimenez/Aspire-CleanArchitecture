using CleanArchitectureSample.Application.Dtos.Request;
using CleanArchitectureSample.Application.Dtos.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Commands;

public class CreateContactCommand(CreateOrUpdateContactRequest contactDto) : IRequest<ContactResponse>
{
    public CreateOrUpdateContactRequest ContactDto { get; } = contactDto ??
        throw new ArgumentNullException(nameof(contactDto));

}
