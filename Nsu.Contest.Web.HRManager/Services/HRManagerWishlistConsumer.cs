using MassTransit;
using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.Common.Message;
using Nsu.Contest.Web.HRManager.Model.Data;

namespace Nsu.Contest.Web.HRManager.Services;

public class HRManagerWishlistConsumer(HRManagerDbContext _context) : IConsumer<WishlistMessage>
{
    public async Task Consume(ConsumeContext<WishlistMessage> context)
    {
        var wishlist = context.Message.Wishlist;
        if(wishlist.EmployeeType == "juniour")
        {
            _context.Juniors.Add((Junior)wishlist.ForEmployee);
        }
        else if(wishlist.EmployeeType == "teamlead")
        {
            _context.Teamleads.Add((Teamlead)wishlist.ForEmployee);
        }
        else
        {
            throw new Exception("Unknown employee type");
        }

        _context.Wishlists.Add(wishlist);
        await _context.SaveChangesAsync();
    }
}
