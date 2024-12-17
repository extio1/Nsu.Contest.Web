namespace Nsu.Contest.Web.HRManager.Services;

using Nsu.Contest.Web.Common.Entity;

public interface IHRManagerService
{
    public void HandleJuniorWishlist(Junior junior, Wishlist wishlist);
    public void HandleTeamleadWishlist(Teamlead teamlead, Wishlist wishlist);
}
