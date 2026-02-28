using MediatR;

namespace BimMarket.Application.Admin.Branches.Queries;

public record GetBranchesQuery : IRequest<List<BranchDto>>;
