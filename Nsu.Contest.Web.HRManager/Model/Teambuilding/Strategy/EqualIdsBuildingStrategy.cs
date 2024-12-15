namespace Nsu.Contest.Web.HRManager.Model.Teambuilding.Strategy;

using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.HRManager.Model.Teambuilding;

public sealed class EqualIdsBuildingStrategy : ITeamBuildingStrategy
{
    public EqualIdsBuildingStrategy(){}
    public IEnumerable<Team> BuildTeams(
            IEnumerable<Teamlead> teamleads, IEnumerable<Junior> juniors,
            IEnumerable<Wishlist> teamleadsWishlists, IEnumerable<Wishlist> juniorsWishlists
        )
    {
        if((teamleads.Count() != juniors.Count())  || 
           (teamleadsWishlists.Count() != juniorsWishlists.Count()) || 
           (teamleads.Count() != teamleadsWishlists.Count()))
        {
            throw new ArgumentException("All collections must be the same length.");
        }

        var employeeCount = teamleads.Count();

        var teams = new List<Team>();

        var teamleadsList = new List<Teamlead>(teamleads);
        var juniorsList = new List<Junior>(juniors);

        foreach (var teamlead in teamleadsList)
        {
            var junior = juniorsList.Find(j => teamlead.Id == j.Id);
            teams.Add(new Team(teamlead, junior));
        }

        return teams;
    }
}

