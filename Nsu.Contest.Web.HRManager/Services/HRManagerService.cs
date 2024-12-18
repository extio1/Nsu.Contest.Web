namespace Nsu.Contest.Web.HRManager.Services;

using Nsu.Contest.Web.HRManager.Model.Data;
using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.HRManager.Model.Teambuilding;
using Microsoft.Extensions.Options;
using Nsu.Contest.Web.HRManager.Model.Config;
using Nsu.Contest.Web.HRManager.Clients;
using Nsu.Contest.Web.HRDirector.Controllers;

public class HRManagerService(
    HRManagerDbContext context, Manager manager, IHRDirectorClient client,
    IOptions<HRManagerConfig> config) : IHRManagerService
{
    private readonly HRManagerDbContext _context = context;
    private readonly Manager _manager = manager;
    private readonly IOptions<HRManagerConfig> _config = config;
    private readonly IHRDirectorClient _client = client;

    private void SaveWishlist(Wishlist wishlist)
    {
        _context.Wishlists.Add(wishlist);
    }

    private void SaveTeamlead(Teamlead teamlead)
    {
        _context.Teamleads.Add(teamlead);
    }

    private void SaveJunior(Junior junior)
    {
        _context.Juniors.Add(junior);
    }

    public void HandleTeamleadWishlist(Teamlead teamlead, Wishlist wishlist)
    {
        SaveTeamlead(teamlead);
        SaveWishlist(wishlist);
        CreateTeamsAndSend();
    }

    public void HandleJuniorWishlist(Junior junior, Wishlist wishlist)
    {
        SaveJunior(junior);
        SaveWishlist(wishlist);
        CreateTeamsAndSend();
    }

    public void CreateTeamsAndSend()
    {
        var teamleads = _context.Teamleads.ToList();
        var juniors = _context.Juniors.ToList();
        var wishlistTeamleads = _context.Wishlists
            .Where(w => teamleads.Contains(w.ForEmployee))
            .ToList();
        var wishlistJuniors = _context.Wishlists
            .Where(w => juniors.Contains(w.ForEmployee))
            .ToList();

        Console.WriteLine(_config.Value.NumberOfTeams);
        if( teamleads.Count         == _config.Value.NumberOfTeams &&
            juniors.Count           == _config.Value.NumberOfTeams &&
            wishlistTeamleads.Count == _config.Value.NumberOfTeams &&
            wishlistJuniors.Count   == _config.Value.NumberOfTeams ) 
        {
            var teams = _manager.BuildTeams(teamleads, juniors, wishlistTeamleads, wishlistJuniors);
            _client.SubmitData(new HRDirectorRequest(wishlistJuniors, wishlistTeamleads, teams));
        }

        _context.SaveChanges();
    }

}
