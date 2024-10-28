using CleanArchitectureSample.Application.Dto.Request;
using MediatR;
using CleanArchitectureSample.Application.Dto.Response;

namespace CleanArchitectureSample.Application.Cqrs.Countries.Commands;

public class UpdateCountryCommand(CreateOrUpdateCountryRequest countryDto, int countryId) : IRequest<CountryResponse>
{
    public CreateOrUpdateCountryRequest CountryDto { get; } = countryDto ??
        throw new ArgumentNullException(nameof(countryDto));

    public int CountryId { get; } = countryId;
}
