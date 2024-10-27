using CleanArchitectureSample.Application.Contracts;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Commands;

public class DeleteContactCommandHandler(IContactRepository contactRepository)
    : IRequestHandler<DeleteContactCommand, bool>
{
    private readonly IContactRepository _contactRepository = contactRepository ??
        throw new ArgumentNullException(nameof(contactRepository));

    public async Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        if (!_contactRepository.Exists(request.Id))
            return false;

        _contactRepository.Delete(request.Id);
        await _contactRepository.SaveAsync();

        return true;
    }
}
