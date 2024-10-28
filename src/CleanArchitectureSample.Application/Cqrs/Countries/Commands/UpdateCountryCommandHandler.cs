using AutoMapper;
using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Application.Dto.Response;
using CleanArchitectureSample.Core.Aggregates;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Countries.Commands;

public class UpdateCountryCommandHandler(ICountryRepository countryRepository, IMapper mapper)
    : IRequestHandler<UpdateCountryCommand, CountryResponse>
{
    private readonly ICountryRepository _countryRepository = countryRepository ??
        throw new ArgumentNullException(nameof(countryRepository));
    private readonly IMapper _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));

    public async Task<CountryResponse> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        if (!countryRepository.Exists(request.CountryId))
            return default!;

        var contact = _mapper.Map<CountryEntity>(request.CountryDto);
        contact.Id = request.CountryId;

        countryRepository.Update(contact);
        await countryRepository.SaveAsync();

        return mapper.Map<CountryResponse>(await countryRepository.GetByIdAsync(contact.Id));
    }
}