using CleanArchitectureSample.Application.Contracts;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.FakeData.Commands;

public class AddFakeContactsCommandHandler(IContactRepository contactRepository) : IRequestHandler<AddFakeContactsCommand>
{
    private readonly IContactRepository _contactRepository = contactRepository ??
        throw new ArgumentNullException(nameof(contactRepository));

    public Task Handle(AddFakeContactsCommand request, CancellationToken cancellationToken)
    {
        contactRepository.CreateFakeContacts(request.NumberToAdd);
        return Task.CompletedTask;
    }
}
