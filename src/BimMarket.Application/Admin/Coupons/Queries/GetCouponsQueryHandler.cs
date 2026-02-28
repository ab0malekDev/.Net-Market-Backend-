using BimMarket.Application.Admin.Coupons;
using BimMarket.Application.Common.Abstractions;
using MediatR;

namespace BimMarket.Application.Admin.Coupons.Queries;

public class GetCouponsQueryHandler(ICouponRepository repo) : IRequestHandler<GetCouponsQuery, List<CouponDto>>
{
    public Task<List<CouponDto>> Handle(GetCouponsQuery request, CancellationToken ct) =>
        repo.GetAllAsync(ct);
}
