using CleanArchitectureSample.Application.Cqrs.FakeData.Commands;
using CleanArchitectureSample.Application.Dto.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureSample.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FakeDataController(ILogger<ContactController> logger, IMediator mediator) : ControllerBase
{
    private readonly ILogger<ContactController> _logger = logger ??
        throw new ArgumentNullException(nameof(logger));
    private readonly IMediator _mediator = mediator ??
        throw new ArgumentNullException(nameof(mediator));


    [HttpPost("contacts")]
    [ProducesResponseType<IEnumerable<ContactResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult> AddFakeContacts([FromBody] int numberToAdd)
    {
        await _mediator.Send(new AddFakeContactsCommand(numberToAdd));
        return Ok();
    }
}
