using CleanArchitectureSample.Application.Contracts;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Commands;

public class DeleteContactCommandHandler(
    IContactRepository contactRepository,
    ICacheService cacheService)
    : IRequestHandler<DeleteContactCommand, bool>
{
    private readonly IContactRepository _contactRepository = contactRepository ??
        throw new ArgumentNullException(nameof(contactRepository));
    private readonly ICacheService _cacheService = cacheService ??
        throw new ArgumentNullException(nameof(cacheService));

    public async Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        _contactRepository.Delete(request.Id);
        await _contactRepository.SaveAsync();

        await _cacheService.RemoveAsync(string.Format(ApplicationConstants.Cache.ContactItem, request.Id));
        await _cacheService.RemoveAsync(ApplicationConstants.Cache.ContactList);

        return true;
    }
}
