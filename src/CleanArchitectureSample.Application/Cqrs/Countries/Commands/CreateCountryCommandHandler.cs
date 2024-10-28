using AutoMapper;
using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Application.Dto.Response;
using CleanArchitectureSample.Core.Aggregates;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Countries.Commands;

public class CreateCountryCommandHandler(ICountryRepository countryRepository, IMapper mapper)
: IRequestHandler<CreateCountryCommand, CountryResponse>
{
    private readonly ICountryRepository _countryRepository = countryRepository ??
        throw new ArgumentNullException(nameof(countryRepository));
    private readonly IMapper _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));

    public async Task<CountryResponse> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = mapper.Map<CountryEntity>(request.CountryDto);

        countryRepository.Create(country);
        await countryRepository.SaveAsync();

        return mapper.Map<CountryResponse>(country);
    }
}
