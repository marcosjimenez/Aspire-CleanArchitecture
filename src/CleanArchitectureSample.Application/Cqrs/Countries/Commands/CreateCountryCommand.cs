using CleanArchitectureSample.Application.Dto.Request;
using CleanArchitectureSample.Application.Dto.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Countries.Commands;

public class CreateCountryCommand(CreateOrUpdateCountryRequest countryDto) : IRequest<CountryResponse>
{
    public CreateOrUpdateCountryRequest CountryDto { get; } = countryDto ??
        throw new ArgumentNullException(nameof(countryDto));

}
