using AutoMapper;
using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Application.Dto.Response;
using CleanArchitectureSample.Core.Aggregates;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Commands;

public class UpdateContactCommandHandler(
    IContactRepository contactRepository,
    ICountryRepository countryRepository,
    ICacheService cacheService,
    IMapper mapper)
    : IRequestHandler<UpdateContactCommand, ContactResponse>
{
    private readonly IContactRepository _contactRepository = contactRepository ??
        throw new ArgumentNullException(nameof(contactRepository));
    private readonly ICountryRepository _countryRepository = countryRepository ??
        throw new ArgumentNullException(nameof(countryRepository));
    private readonly IMapper _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));
    private readonly ICacheService _cacheService = cacheService ??
        throw new ArgumentNullException(nameof(cacheService));

    public async Task<ContactResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        if (!contactRepository.Exists(request.ContactId))
            return default!;

        var contact = _mapper.Map<ContactEntity>(request.ContactDto);
        contact.Id = request.ContactId;

        if (request.ContactDto.CountryId.HasValue)
        {
            if (!countryRepository.Exists(request.ContactDto.CountryId.Value))
                return default!;
        }

        contactRepository.Update(contact);
        await contactRepository.SaveAsync();

        await _cacheService.SetAsync(string.Format(ApplicationConstants.Cache.ContactItem, contact.Id), contact);
        await _cacheService.RemoveAsync(ApplicationConstants.Cache.ContactList);

        return mapper.Map<ContactResponse>(await contactRepository.GetByIdWithIncludesAsync(contact.Id));
    }
}
