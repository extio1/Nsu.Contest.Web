using MassTransit;
using Nsu.Contest.Web.Common.Message;

namespace Nsu.Contest.Web.Employee.Services;

public class ContestStartConsumer(IBus _bus, WishlistProducer _wishlistProducer) : IConsumer<ContestStartMessage>
{
    public async Task Consume(ConsumeContext<ContestStartMessage> context)
    {
        var wishlist = _wishlistProducer.ProduceWishlist(context.Message.Id);
        await _bus.Publish(new WishlistMessage{Wishlist = wishlist});
    }
}
