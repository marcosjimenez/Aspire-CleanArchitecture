using AutoMapper;
using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Application.Dtos.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Countries.Queries;

public class GetAllCountriesQueryHandler(ICountryRepository countryRepository, IMapper mapper)
    : IRequestHandler<GetAllCountriesQuery, IEnumerable<CountryResponse>>
{
    private readonly ICountryRepository _countryRepository = countryRepository ??
          throw new ArgumentNullException(nameof(countryRepository));
    private readonly IMapper _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));

    public async Task<IEnumerable<CountryResponse>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var response = new List<CountryResponse>();
        var contacts = await _countryRepository.GetAllAsync();
        if (contacts is not null)
        {
            response = _mapper.Map<List<CountryResponse>>(contacts);
        }
        return response;
    }
}