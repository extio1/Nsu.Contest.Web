using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

using Nsu.Contest.Web.HRManager.Model.Config;
using Nsu.Contest.Web.HRManager.Clients;
using Nsu.Contest.Web.HRManager.Model.Data;
using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.HRDirector.Model;
using Nsu.Contest.Web.HRManager.Model.Teambuilding;
using Nsu.Contest.Web.HRDirector.Controllers;

namespace HrManager.Service;

public class HRManagerBackgrond(
    IHRDirectorClient _client, 
    HRManagerDbContext _context, Manager _manager,
    IOptions<HRManagerConfig> _options) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(_options.Value.SenderInterval), cancellationToken);

            var wishlistGroups = _context.Wishlists.GroupBy(w => w.HackatonId).ToList();

            foreach (var group in wishlistGroups)
            {
                var wishlists = group.ToList();
                if(wishlists.Count == _options.Value.NumberOfTeams*2)
                {
                    var juniors = wishlists.Where(w => w.EmployeeType == "junior").Select(w => (Junior)w.ForEmployee);
                    var teamleads = wishlists.Where(w => w.EmployeeType == "teamlead").Select(w => (Teamlead)w.ForEmployee);
                    var wishlistTeamleads = _context.Wishlists
                        .Where(w => teamleads.Contains(w.ForEmployee))
                        .ToList();
                    var wishlistJuniors = _context.Wishlists
                        .Where(w => juniors.Contains(w.ForEmployee))
                        .ToList();
                    
                    var teams = _manager.BuildTeams(teamleads, juniors, wishlistTeamleads, wishlistJuniors);
                    await _client.SubmitData(new HRDirectorRequest(teams));
                }
            }
        }
    }
}