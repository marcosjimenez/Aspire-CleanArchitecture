using CleanArchitectureSample.Application.Dtos.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Countries.Queries;

public class GetAllCountriesQuery : IRequest<IEnumerable<CountryResponse>>
{
}
