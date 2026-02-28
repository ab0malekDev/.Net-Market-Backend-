using MediatR;

namespace BimMarket.Application.Admin.Coupons.Queries;

public record GetCouponsQuery : IRequest<List<CouponDto>>;
