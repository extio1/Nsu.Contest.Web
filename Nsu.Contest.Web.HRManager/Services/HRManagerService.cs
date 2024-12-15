namespace Nsu.Contest.Web.HRManager.Services;

using Nsu.Contest.Web.HRManager.Model.Data;
using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.HRManager.Model.Teambuilding;
using Microsoft.Extensions.Options;
using Nsu.Contest.Web.HRManager.Model.Config;

public class HRManagerService
    {
        private readonly HRManagerDbContext _context;
        private readonly Manager _manager;
        private readonly IOptions<HRManagerConfig> _config;

        public HRManagerService(HRManagerDbContext context, Manager manager, IOptions<HRManagerConfig> config)
        {
            _context = context;
            _manager = manager;
            _config = config;
        }

        public async Task SaveWishlistAsync(Wishlist wishlist)
        {
            if (!_context.Wishlists.Any(w => w.Id == wishlist.Id))
            {
                _context.Wishlists.Add(wishlist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveTeamleadWishlistAsync(Teamlead teamlead, Wishlist wishlist)
        {
            if (!_context.Teamleads.Any(t => t.Id == teamlead.Id))
            {
                _context.Teamleads.Add(teamlead);
                await SaveWishlistAsync(wishlist);
            }
        }

        public async Task SaveJuniorWishlistAsync(Junior junior, Wishlist wishlist)
        {
            if (!_context.Juniors.Any(j => j.Id == junior.Id))
            {
                _context.Juniors.Add(junior);
                await SaveWishlistAsync(wishlist);
            }
        }

        public async Task CreateTeamsAndSendAsync()
        {
            var teamleads = _context.Teamleads.ToList();
            var juniors = _context.Juniors.ToList();
            var wishlistTeamleads = _context.Wishlists
                .Where(w => teamleads.Contains(w.ForEmployee))
                .ToList();
            var wishlistJuniors = _context.Wishlists
                .Where(w => juniors.Contains(w.ForEmployee))
                .ToList();

            if( teamleads.Count         == _config.Value.NumberOfTeams &&
                juniors.Count           == _config.Value.NumberOfTeams &&
                wishlistTeamleads.Count == _config.Value.NumberOfTeams &&
                wishlistJuniors.Count   == _config.Value.NumberOfTeams ) 
            {
                var teams = _manager.BuildTeams(teamleads, juniors, wishlistTeamleads, wishlistJuniors);
                
                foreach (var item in teams)
                {
                    Console.WriteLine($"{item.Junior.Id}, {item.Teamlead.Id}");
                }

                await _context.SaveChangesAsync();
            }
        }

    }
