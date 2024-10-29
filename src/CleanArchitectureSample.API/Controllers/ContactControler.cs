using MediatR;
using Microsoft.AspNetCore.Mvc;
using CleanArchitectureSample.Application.Dto.Request;
using CleanArchitectureSample.Application.Cqrs.Contacts.Queries;
using CleanArchitectureSample.Application.Cqrs.Contacts.Commands;
using CleanArchitectureSample.Application.Dto.Response;

namespace CleanArchitectureSample.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController(ILogger<ContactController> logger, IMediator mediator) : ControllerBase
{
    private readonly ILogger<ContactController> _logger = logger ??
        throw new ArgumentNullException(nameof(logger));
    private readonly IMediator _mediator = mediator ??
        throw new ArgumentNullException(nameof(mediator));

    [HttpGet]
    [ProducesResponseType<IEnumerable<ContactResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContactResponse>>> GetAll()
    {
        var retVal = await _mediator.Send(new GetAllContactsQuery());
        return Ok(retVal);
    }

    [HttpGet("{contactId}")]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<ContactResponse>> GetById(int contactId)
    {
        var retVal = await _mediator.Send(new GetContactQuery(contactId));
        return retVal is null ?
            NotFound() :
            Ok(retVal);
    }

    [HttpPost]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContactResponse>> CreateContact(CreateOrUpdateContactRequest newContact)
    {
        var retVal = await _mediator.Send(new CreateContactCommand(newContact));
        return CreatedAtAction(nameof(GetById), new { contactId = retVal.Id }, retVal);
    }

    [HttpPut("{contactId}")]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactResponse>> UpdateContact(CreateOrUpdateContactRequest newContact, int contactId)
    {
        var retVal = await _mediator.Send(new UpdateContactCommand(newContact, contactId));

        return (retVal is null) ?
            NotFound() :
            Ok(retVal);
    }

    [HttpDelete("{contactId}")]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteContact(int contactId)
    {
        var retVal = await _mediator.Send(new DeleteContactCommand(contactId));

        return retVal ?
            Ok() :
            NotFound();
    }
}
