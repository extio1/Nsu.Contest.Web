namespace Nsu.Contest.Web.HRManager.Services;

using Nsu.Contest.Web.HRManager.Model.Data;
using Nsu.Contest.Web.Common.Entity;

public class HRManagerService(HRManagerDbContext context) : IHRManagerService
{
    private readonly HRManagerDbContext _context = context;
    private void SaveWishlist(Wishlist wishlist)
    {
        _context.Wishlists.Add(wishlist);
    }

    private void SaveTeamlead(Teamlead teamlead)
    {
        _context.Teamleads.Add(teamlead);
    }

    private void SaveJunior(Junior junior)
    {
        _context.Juniors.Add(junior);
    }

    public void HandleTeamleadWishlist(Teamlead teamlead, Wishlist wishlist)
    {
        SaveTeamlead(teamlead);
        SaveWishlist(wishlist);
    }

    public void HandleJuniorWishlist(Junior junior, Wishlist wishlist)
    {
        SaveJunior(junior);
        SaveWishlist(wishlist);
    }

}
