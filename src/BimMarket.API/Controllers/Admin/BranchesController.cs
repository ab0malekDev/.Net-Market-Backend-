using BimMarket.Application.Admin.Branches.Commands;
using BimMarket.Application.Admin.Branches.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BimMarket.API.Controllers.Admin;

[ApiController]
[Route("api/admin/branches")]
// [Authorize] // temporarily disabled to verify route is matched
public class BranchesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BranchesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetBranches(CancellationToken ct = default)
    {
        var list = await _mediator.Send(new GetBranchesQuery(), ct);
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBranch([FromBody] CreateBranchCommand command, CancellationToken ct = default)
    {
        var created = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetBranches), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBranch(string id, [FromBody] UpdateBranchCommand body, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out _))
            return BadRequest(new { error = "Invalid branch id format." });

        var updated = await _mediator.Send(body with { Id = id }, ct);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBranch(string id, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out _))
            return BadRequest(new { error = "Invalid branch id format." });

        var success = await _mediator.Send(new DeleteBranchCommand(id), ct);
        if (!success) return NotFound();
        return NoContent();
    }
}
