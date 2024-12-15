namespace Nsu.Contest.Web.HRManager.Model.Teambuilding.Strategy;

using Nsu.Contest.Web.Common.Util;
using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.HRManager.Model.Teambuilding;

public sealed class RandomTeamBuildingStrategy : ITeamBuildingStrategy
{
    public RandomTeamBuildingStrategy(){}

    /// <summary>
    /// Random algorithm of teambuilding.
    /// </summary>
    /// <param name="teamleads"></param>
    /// <param name="juniors"></param>
    /// <param name="teamleadsWishlists"></param>
    /// <param name="juniorsWishlists"></param>
    /// <returns> Random distribution of team members </returns>
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

        var employeesCount = teamleads.Count();
        var teams = new List<Team>(employeesCount);

        var randPermteamleads = RandomGenerator.GeneratePermutation(employeesCount);
        var randPermJuniors = RandomGenerator.GeneratePermutation(employeesCount);

        var teamleadsList = new List<Teamlead>(teamleads);
        var juniorsList = new List<Junior>(juniors);

        for (var i = 0; i < employeesCount; i++)
        {
            teams.Add(new Team(teamleadsList[randPermteamleads[i] - 1], juniorsList[randPermJuniors[i] - 1]));
        }

        return teams;
    }
}
