namespace Nsu.Contest.Web.HRManager.Services;

using Nsu.Contest.Web.Common.Entity;

public interface IHRManagerService
{
    public Task HandleJuniorWishlistAsync(Junior junior, Wishlist wishlist);
    public Task HandleTeamleadWishlistAsync(Teamlead teamlead, Wishlist wishlist);
}
