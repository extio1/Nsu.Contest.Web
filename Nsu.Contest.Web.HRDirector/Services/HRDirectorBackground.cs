using Microsoft.Extensions.Options;
using Nsu.Contest.Web.HRDirector.Model;
using Nsu.Contest.Web.HRDirector.Model.Data;

namespace Nsu.Contest.Web.HRDirector.Services;

public class HRDirectorBackground(
    IOptions<HRDirectorConfig> _options, HRDirectorDbContext _context, Director _director) 
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(_options.Value.BackgrondWaitInterval), cancellationToken);

            var groupedTeams = _context.Teams.GroupBy(t => t.HackatonId)
                                            .Select(g => g.ToList())
                                            .ToList();

            foreach(var teams in groupedTeams) 
            {
                if(teams.Count == _options.Value.ParticipantCount)
                {
                    var teamleads = teams.Select(t => t.Teamlead);
                    var juniors = teams.Select(t => t.Junior);
                    var wishlists = _context.Wishlists.ToList();
                    var teamleadWishlists = wishlists.Where(w => teamleads.Contains(w.ForEmployee));
                    var juniorWishlists = wishlists.Where(w => teamleads.Contains(w.ForEmployee));
                
                    var score = _director.EstimateTeams(juniorWishlists, teamleadWishlists, teams);

                    _context.Teams.RemoveRange(teams);
                    _context.Wishlists.RemoveRange(juniorWishlists);
                    _context.Wishlists.RemoveRange(teamleadWishlists);
                }
            }
        }
    }
}
