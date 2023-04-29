using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared.Order;

namespace BlazorEcommerce.Application.Features.Order.Query.GetOrder;

public record GetOrderQueryRequest : IRequest<IResponse>;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;
    private readonly ICurrentUser _currentUser;

    public GetOrderQueryHandler(IQueryUnitOfWork query, ICurrentUser currentUser)
    {
        _query = query;
        _currentUser = currentUser;
    }

    public async Task<IResponse> Handle(GetOrderQueryRequest request, CancellationToken cancellationToken)
    {
        var orderLists = await _query.OrderQuery.GetAllOrderByUserId(_currentUser.UserId);

        var orderResponse = new List<OrderOverviewResponse>();

        orderLists.ForEach(o => orderResponse.Add(new OrderOverviewResponse
        {
            Id = o.Id,
            OrderDate = o.OrderDate,
            TotalPrice = o.TotalPrice,
            Product = o.OrderItems.Count > 1 ?
                $"{o.OrderItems.First().Product.Title} and" +
                $" {o.OrderItems.Count - 1} more..." :
                o.OrderItems.First().Product.Title,
            ProductImageUrl = o.OrderItems.First().Product.ImageUrl
        }));

        return new DataResponse<List<OrderOverviewResponse>>(orderResponse, HttpStatusCodes.Accepted);
    }
}
