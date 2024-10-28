using CleanArchitectureSample.Application.Dto.Request;
using CleanArchitectureSample.Application.Dto.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Commands;

public class UpdateContactCommand(CreateOrUpdateContactRequest contactDto, int contactId) : IRequest<ContactResponse>
{
    public CreateOrUpdateContactRequest ContactDto { get; } = contactDto ??
        throw new ArgumentNullException(nameof(contactDto));

    public int ContactId { get; } = contactId;
}
