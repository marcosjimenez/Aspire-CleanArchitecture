using AutoMapper;
using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Application.Dto.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Countries.Queries;

public class GetCountryQueryHandler(ICountryRepository countryRepository, IMapper mapper)
    : IRequestHandler<GetCountryQuery, CountryResponse>
{
    private readonly ICountryRepository _countryRepository = countryRepository ??
          throw new ArgumentNullException(nameof(countryRepository));
    private readonly IMapper _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));

    public async Task<CountryResponse> Handle(GetCountryQuery request, CancellationToken cancellationToken)
    {
        var contact = await _countryRepository.GetByIdWithIncludesAsync(request.Id);

        return _mapper.Map<CountryResponse>(contact);
    }
}