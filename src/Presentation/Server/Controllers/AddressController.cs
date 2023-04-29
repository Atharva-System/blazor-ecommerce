using BlazorEcommerce.Application.Features.Address.Command.AddAddress;
using BlazorEcommerce.Application.Features.Address.Query.GetAddress;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AddressController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IResponse>> GetAddress()
    {
        var response = await _mediator.Send(new GetAddressQueryRequest());
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<IResponse>> AddOrUpdateAddress(AddressDto address)
    {
        var response = await _mediator.Send(new AddOrUpdateCommandRequest(address));
        return Ok(response);
    }
}

