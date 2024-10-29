using CleanArchitectureSample.Application.Dto.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Countries.Queries;

public class GetAllCountriesQuery : IRequest<IEnumerable<CountryResponse>>
{
}
