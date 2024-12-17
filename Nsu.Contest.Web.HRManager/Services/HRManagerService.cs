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

        private void SaveWishlist(Wishlist wishlist)
        {
            if (!_context.Wishlists.Any(w => w.Id == wishlist.Id))
            {
                _context.Wishlists.Add(wishlist);
            }
        }

        private void SaveTeamlead(Teamlead teamlead)
        {
            if (!_context.Teamleads.Any(t => t.Id == teamlead.Id))
            {
                _context.Teamleads.Add(teamlead);
            }
        }

        private void SaveJunior(Junior junior)
        {
            if (!_context.Juniors.Any(j => j.Id == junior.Id))
            {
                _context.Juniors.Add(junior);
            }
        }

        public async Task HandleTeamleadWishlistAsync(Teamlead teamlead, Wishlist wishlist)
        {
            SaveTeamlead(teamlead);
            SaveWishlist(wishlist);
            await CreateTeamsAndSendAsync();
        }

        public async Task HandleJuniorWishlistAsync(Junior junior, Wishlist wishlist)
        {
            SaveJunior(junior);
            SaveWishlist(wishlist);
            await CreateTeamsAndSendAsync();
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
