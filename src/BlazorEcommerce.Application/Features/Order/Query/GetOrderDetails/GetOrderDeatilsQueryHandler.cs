using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared.Order;

namespace BlazorEcommerce.Application.Features.Order.Query.GetOrderDetails;

public record GetOrderDeatilsQueryRequest(int orderId) : IRequest<IResponse>;

public class GetOrderDeatilsQueryHandler : IRequestHandler<GetOrderDeatilsQueryRequest, IResponse>
{
    private readonly IQueryUnitOfWork _query;
    private readonly ICurrentUser _currentUser;

    public GetOrderDeatilsQueryHandler(IQueryUnitOfWork query, ICurrentUser currentUser)
    {
        _query = query;
        _currentUser = currentUser;
    }

    public async Task<IResponse> Handle(GetOrderDeatilsQueryRequest request, CancellationToken cancellationToken)
    {
        var order = await _query.OrderQuery.GetOrderDetails(_currentUser.UserId, request.orderId);

        if (order == null)
        {
            return new DataResponse<string?>(null, HttpStatusCodes.NotFound, String.Format(Messages.NotFound, "Order"), false);
        }

        var orderDetailsResponse = new OrderDetailsResponse
        {
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice,
            Products = new List<OrderDetailsProductResponse>()
        };

        order.OrderItems.ForEach(item =>
        orderDetailsResponse.Products.Add(new OrderDetailsProductResponse
        {
            ProductId = item.ProductId,
            ImageUrl = item.Product.ImageUrl,
            ProductType = item.ProductType.Name,
            Quantity = item.Quantity,
            Title = item.Product.Title,
            TotalPrice = item.TotalPrice
        }));

        return new DataResponse<OrderDetailsResponse>(orderDetailsResponse,HttpStatusCodes.Accepted);
    }
}
