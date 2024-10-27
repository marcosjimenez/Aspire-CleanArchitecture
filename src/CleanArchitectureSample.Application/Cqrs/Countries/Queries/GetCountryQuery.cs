using CleanArchitectureSample.Application.Dtos.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Countries.Queries;

public class GetCountryQuery(int id) : IRequest<CountryResponse>
{
    public int Id { get; } = id;
}
