namespace Nsu.Contest.Web.HRDirector.Services;

using MassTransit;
using Nsu.Contest.Web.Common.Messages;
using Nsu.Contest.Web.HRDirector.Model.Data;

public class HRDirectorTeamConsumer(HRDirectorDbContext _context) : IConsumer<WishilistMessage>
{
    public async Task Consume(ConsumeContext<WishilistMessage> context)
    {
        await _context.Wishlists.AddAsync(context.Message.Wishlist);
    }
}