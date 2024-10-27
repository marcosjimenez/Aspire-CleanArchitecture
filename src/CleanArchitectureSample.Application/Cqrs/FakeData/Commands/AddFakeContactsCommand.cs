using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.FakeData.Commands;

public class AddFakeContactsCommand(int numberToAdd) : IRequest
{
    public int NumberToAdd { get; protected set; } = numberToAdd;

}
