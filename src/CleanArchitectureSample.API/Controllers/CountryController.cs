using CleanArchitectureSample.Application.Cqrs.Contacts.Commands;
using CleanArchitectureSample.Application.Cqrs.Countries.Commands;
using CleanArchitectureSample.Application.Cqrs.Countries.Queries;
using CleanArchitectureSample.Application.Dtos.Request;
using CleanArchitectureSample.Application.Dtos.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureSample.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController(ILogger<ContactController> logger, IMediator mediator) : ControllerBase
{
    private readonly ILogger<ContactController> _logger = logger ??
        throw new ArgumentNullException(nameof(logger));
    private readonly IMediator _mediator = mediator ??
        throw new ArgumentNullException(nameof(mediator));


    [HttpGet]
    [ProducesResponseType<IEnumerable<CountryResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CountryResponse>>> GetAll()
    {
        var retVal = await _mediator.Send(new GetAllCountriesQuery());
        return Ok(retVal);
    }

    [HttpGet("{countryId}")]
    [ProducesResponseType<CountryResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<CountryResponse>> GetById(int countryId)
    {
        var retVal = await _mediator.Send(new GetCountryQuery(countryId));
        return retVal is null ?
            NotFound() :
            Ok(retVal);
    }

    [HttpPost]
    [ProducesResponseType<CountryResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<CountryResponse>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CountryResponse>> CreateContact(CreateOrUpdateCountryRequest newCountry)
    {
        var retVal = await _mediator.Send(new CreateCountryCommand(newCountry));
        return CreatedAtAction(nameof(GetById), new { countryId = retVal.Id }, retVal);
    }

    [HttpPut("{countryId}")]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CountryResponse>> UpdateCountry(CreateOrUpdateCountryRequest newCountry, int countryId)
    {
        var retVal = await _mediator.Send(new UpdateCountryCommand(newCountry, countryId));

        return (retVal is null) ?
            NotFound() :
            Ok(retVal);
    }

    [HttpDelete("{countryId}")]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ContactResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteCountry(int countryId)
    {
        var retVal = await _mediator.Send(new DeleteCountryCommand(countryId));

        return retVal ?
            Ok() :
            NotFound();
    }
}
