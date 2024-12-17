namespace Nsu.Contest.Web.HRDirector.Services;

using Nsu.Contest.Web.Common.Entity;

public interface IHRDirectorService
{
    public void HandleWishlistsTeams(
        IEnumerable<Wishlist> juniorWishlists, 
        IEnumerable<Wishlist> teamleadWishlists, 
        IEnumerable<Team> teams
    );
}
