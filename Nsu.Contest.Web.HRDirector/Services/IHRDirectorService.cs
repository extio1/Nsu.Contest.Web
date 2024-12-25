namespace Nsu.Contest.Web.HRDirector.Services;

using Nsu.Contest.Web.Common.Entity;

public interface IHRDirectorService
{
    public void HandleTeams(
        IEnumerable<Team> teams
    );
}
