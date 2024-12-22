// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.DependencyInjection;
// using System;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.Extensions.Options;

// using Polly;
// using Polly.Retry;

// using Nsu.Contest.Web.HRManager.Model.Config;
// using Nsu.Contest.Web.HRManager.Clients;
// using Nsu.Contest.Web.HRManager.Model.Data;
// using Nsu.Contest.Web.Common.Entity;
// using Nsu.Contest.Web.HRDirector.Model;
// using Nsu.Contest.Web.HRManager.Model.Teambuilding;
// using Nsu.Contest.Web.HRDirector.Controllers;

// namespace HrManager.Service;

// public class HRManagerBackgrond(IHRDirectorClient _client, HRManagerDbContext _context, Manager _manager,
//     IOptions<HRManagerConfig> _options, IServiceScopeFactory _serviceScopeFactory) : BackgroundService
// {
//     protected override async Task ExecuteAsync(CancellationToken cancellationToken)
//     {
//         while (!cancellationToken.IsCancellationRequested)
//         {
//             await Task.Delay(TimeSpan.FromSeconds(_options.Value.SenderInterval), cancellationToken);
//             using (_serviceScopeFactory.CreateScope())
//             {
//                 var juniors = _context.Juniors.ToList();
//                 var teamleads = _context.Teamleads.ToList();

//                 Console.WriteLine($"juniors: {juniors.Count()}, teamleads: {teamleads.Count()}");

//                 if (juniors.Count   == _options.Value.NumberOfTeams &&
//                     teamleads.Count == _options.Value.NumberOfTeams)
//                 {
//                     var wishlistTeamleads = _context.Wishlists
//                         .Where(w => teamleads.Contains(w.ForEmployee))
//                         .ToList();
//                     var wishlistJuniors = _context.Wishlists
//                         .Where(w => juniors.Contains(w.ForEmployee))
//                         .ToList();

//                     var teams = _manager.BuildTeams(teamleads, juniors, wishlistTeamleads, wishlistJuniors);
//                     Console.WriteLine("Send teams to HrDirector");
//                     await _client.SubmitData(new HRDirectorRequest(wishlistJuniors, wishlistTeamleads, teams));
//                     Console.WriteLine("Hackathon was marked as finished");
//                     return;
//                 }
//             }
//         }
//     }
// }