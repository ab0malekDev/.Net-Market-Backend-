using BimMarket.Application.Admin.Branches;
using BimMarket.Application.Common.Abstractions;
using MediatR;

namespace BimMarket.Application.Admin.Branches.Queries;

public class GetBranchesQueryHandler(IBranchRepository repo) : IRequestHandler<GetBranchesQuery, List<BranchDto>>
{
    public Task<List<BranchDto>> Handle(GetBranchesQuery request, CancellationToken ct) =>
        repo.GetAllAsync(ct);
}
