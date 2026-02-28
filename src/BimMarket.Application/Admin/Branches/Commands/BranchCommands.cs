using BimMarket.Application.Common.Abstractions;
using MediatR;

namespace BimMarket.Application.Admin.Branches.Commands;

public record CreateBranchCommand(
    string Name,
    string Address,
    string City,
    string? Region,
    string? Phone,
    bool IsActive,
    int SortOrder) : IRequest<BranchDto>;

public record UpdateBranchCommand(
    string Id,
    string Name,
    string Address,
    string City,
    string? Region,
    string? Phone,
    bool IsActive,
    int SortOrder) : IRequest<BranchDto?>;

public record DeleteBranchCommand(string Id) : IRequest<bool>;

public class CreateBranchCommandHandler(IBranchRepository repo) : IRequestHandler<CreateBranchCommand, BranchDto>
{
    public Task<BranchDto> Handle(CreateBranchCommand request, CancellationToken ct) =>
        repo.CreateAsync(request.Name, request.Address, request.City, request.Region, request.Phone, request.IsActive, request.SortOrder, ct);
}

public class UpdateBranchCommandHandler(IBranchRepository repo) : IRequestHandler<UpdateBranchCommand, BranchDto?>
{
    public Task<BranchDto?> Handle(UpdateBranchCommand request, CancellationToken ct) =>
        Guid.TryParse(request.Id, out var id)
            ? repo.UpdateAsync(id, request.Name, request.Address, request.City, request.Region, request.Phone, request.IsActive, request.SortOrder, ct)
            : Task.FromResult<BranchDto?>(null);
}

public class DeleteBranchCommandHandler(IBranchRepository repo) : IRequestHandler<DeleteBranchCommand, bool>
{
    public Task<bool> Handle(DeleteBranchCommand request, CancellationToken ct) =>
        Guid.TryParse(request.Id, out var id)
            ? repo.DeleteAsync(id, ct)
            : Task.FromResult(false);
}

