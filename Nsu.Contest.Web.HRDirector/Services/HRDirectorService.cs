namespace Nsu.Contest.Web.HRDirector.Services;

using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.HRDirector.Model;
using Nsu.Contest.Web.HRDirector.Model.Data;

public class HRDirectorService(Director director, HRDirectorDbContext context) : IHRDirectorService
{
    private readonly Director _director = director;
    private readonly HRDirectorDbContext _context = context;
    public void HandleWishlistsTeams(
        IEnumerable<Wishlist> juniorWishlists, 
        IEnumerable<Wishlist> teamleadWishlists, 
        IEnumerable<Team> teams
    )
    {
        var teamsScore = _director.EstimateTeams(juniorWishlists, teamleadWishlists, teams);
        _context.Contests.Add(new Contest(teams, teamsScore));
        _context.SaveChanges();
    }
}
