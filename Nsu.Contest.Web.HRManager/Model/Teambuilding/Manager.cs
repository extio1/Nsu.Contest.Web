namespace Nsu.Contest.Web.HRManager.Model.Teambuilding;

using Nsu.Contest.Web.Common.Entity;

public class Manager
{
    private readonly ITeamBuildingStrategy _teamBuildingStrategy;

    public Manager(ITeamBuildingStrategy teamBuildingStrategy)
    {
        _teamBuildingStrategy = teamBuildingStrategy;
    }

    public IEnumerable<Team> BuildTeams(
        IEnumerable<Teamlead> teamleads, IEnumerable<Junior> juniors,
        IEnumerable<Wishlist> teamleadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        return _teamBuildingStrategy.BuildTeams(teamleads, juniors, teamleadsWishlists, juniorsWishlists);
    }
}
