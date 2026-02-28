using BimMarket.Application.Admin.Reviews.Commands;
using BimMarket.Application.Admin.Reviews.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BimMarket.API.Controllers.Admin;

[ApiController]
[Route("api/admin/reviews")]
// [Authorize]
public class ReviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReviewsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetReviews([FromQuery] bool? approved = null, CancellationToken ct = default)
    {
        var list = await _mediator.Send(new GetReviewsQuery(approved), ct);
        return Ok(list);
    }

    [HttpPatch("{id}/approve")]
    public async Task<IActionResult> Approve(string id, [FromBody] ApproveBody body, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new ApproveReviewCommand(id, body.IsApproved), ct);
        if (result == null) return NotFound();
        return Ok(result);
    }
}

public record ApproveBody(bool IsApproved);
