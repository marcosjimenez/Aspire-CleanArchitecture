using CleanArchitectureSample.Application.Contracts;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Countries.Commands;

public class DeleteCountryCommandHandler(ICountryRepository countryRepository)
    : IRequestHandler<DeleteCountryCommand, bool>
{
    private readonly ICountryRepository _countryRepository = countryRepository ??
        throw new ArgumentNullException(nameof(countryRepository));

    public async Task<bool> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        _countryRepository.Delete(request.Id);
        await _countryRepository.SaveAsync();

        return true;
    }
}