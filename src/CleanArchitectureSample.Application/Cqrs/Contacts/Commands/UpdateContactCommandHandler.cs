using AutoMapper;
using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Application.Dtos.Response;
using CleanArchitectureSample.Core.Aggregates;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Commands;

public class UpdateContactCommandHandler(IContactRepository contactRepository, ICountryRepository countryRepository, IMapper mapper)
    : IRequestHandler<UpdateContactCommand, ContactResponse>
{
    private readonly IContactRepository _contactRepository = contactRepository ??
        throw new ArgumentNullException(nameof(contactRepository));
    private readonly ICountryRepository _countryRepository = countryRepository ??
        throw new ArgumentNullException(nameof(countryRepository));
    private readonly IMapper _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));

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

        return mapper.Map<ContactResponse>(await contactRepository.GetByIdWithIncludesAsync(contact.Id));
    }
}
