using BimMarket.Application.Admin.Coupons.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BimMarket.API.Controllers.Admin;

[ApiController]
[Route("api/admin/coupons")]
// [Authorize]
public class CouponsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CouponsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetCoupons(CancellationToken ct = default)
    {
        var list = await _mediator.Send(new GetCouponsQuery(), ct);
        return Ok(list);
    }
}
