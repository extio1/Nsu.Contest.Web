namespace Nsu.Contest.Web.HRDirector.Services;

using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.HRDirector.Model;
using Nsu.Contest.Web.HRDirector.Model.Data;

public class HRDirectorService(HRDirectorDbContext _context) : IHRDirectorService
{
    public void HandleTeams(IEnumerable<Team> teams)
    {
        foreach (var team in teams)
        {
            _context.Teams.Add(new Team(team.HackatonId, team.Teamlead, team.Junior));   
        }
        _context.SaveChanges();
    }
}
