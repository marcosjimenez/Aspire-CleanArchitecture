﻿using AutoMapper;
using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Application.Dtos.Response;
using CleanArchitectureSample.Core.Aggregates;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Commands;

public class CreateContactCommandHandler(IContactRepository contactRepository, ICountryRepository countryRepository, IMapper mapper)
    : IRequestHandler<CreateContactCommand, ContactResponse>
{
    private readonly IContactRepository _contactRepository = contactRepository ??
        throw new ArgumentNullException(nameof(contactRepository));
    private readonly ICountryRepository _countryRepository = countryRepository ??
        throw new ArgumentNullException(nameof(countryRepository));
    private readonly IMapper _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));

    public async Task<ContactResponse> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = mapper.Map<ContactEntity>(request.ContactDto);

        contact.Country = request.ContactDto.CountryId.HasValue ?
             countryRepository.GetById(request.ContactDto.CountryId.Value) :
             null;

        contactRepository.Create(contact);
        await contactRepository.SaveAsync();

        return mapper.Map<ContactResponse>(contact);
    }
}