using BimMarket.Application.Admin.FlashDeals.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BimMarket.API.Controllers.Admin;

[ApiController]
[Route("api/admin/flashdeals")]
// [Authorize]
public class FlashDealsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FlashDealsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetFlashDeals(CancellationToken ct = default)
    {
        var list = await _mediator.Send(new GetFlashDealsQuery(), ct);
        return Ok(list);
    }
}
