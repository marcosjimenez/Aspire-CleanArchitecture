using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Commands;

public class DeleteContactCommand(int id) : IRequest<bool>
{
    public int Id { get; } = id;
}
