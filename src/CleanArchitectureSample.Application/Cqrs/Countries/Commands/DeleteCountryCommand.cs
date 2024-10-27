using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Countries.Commands;

public class DeleteCountryCommand(int id) : IRequest<bool>
{
    public int Id { get; } = id;
}
