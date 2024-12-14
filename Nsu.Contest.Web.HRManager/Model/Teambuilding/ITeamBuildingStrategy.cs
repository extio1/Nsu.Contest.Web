namespace Nsu.Contest.Web.HRManager.Model.Teambuilding;

using Nsu.Contest.Web.Common.Entity;

public interface ITeamBuildingStrategy
{
    /// <summary>
    /// Распределяет тимлидов и джунов по командам
    /// </summary>
    /// <param name="teamLeads">Тимлиды</param>
    /// <param name="juniors">Джуны</param>
    /// <returns>Список команд</returns>
    IEnumerable<Team> BuildTeams(
        IEnumerable<Teamlead> teamleads, IEnumerable<Junior> juniors,
        IEnumerable<Wishlist> teamleadsWishlists, IEnumerable<Wishlist> juniorsWishlists
    );
}
