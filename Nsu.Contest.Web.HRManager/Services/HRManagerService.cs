namespace Nsu.Contest.Web.HRManager.Services;

public class HRManagerService
    {
        // private readonly HRManagerDbContext _context;

        // public HRManagerService(HRManagerDbContext context)
        // {
        //     _context = context;
        // }

        public HRManagerService(){}

        // // Сохраняем информацию о сотруднике
        // public async Task SaveEmployeeAsync(Employee employee)
        // {
        //     if (!_context.Employees.Any(e => e.Id == employee.Id))
        //     {
        //         _context.Employees.Add(employee);
        //         await _context.SaveChangesAsync();
        //     }
        // }

        // // Формируем команды
        // public async Task<List<Team>> CreateTeamsAsync()
        // {
        //     var juniors = _context.Employees.Where(e => e.Type == "junior").ToList();
        //     var teamLeads = _context.Employees.Where(e => e.Type == "teamlead").ToList();

        //     var teams = new List<Team>();

        //     foreach (var junior in juniors)
        //     {
        //         var preferredTeamLeadId = junior.Wishlist.FirstOrDefault(tl => teamLeads.Any(t => t.Id == tl));
        //         var teamLead = teamLeads.FirstOrDefault(tl => tl.Id == preferredTeamLeadId);

        //         if (teamLead != null)
        //         {
        //             teams.Add(new Team { Junior = junior, TeamLead = teamLead });
        //             teamLeads.Remove(teamLead); // Убираем teamlead, чтобы он был в одной команде
        //         }
        //     }

        //     _context.Teams.AddRange(teams);
        //     await _context.SaveChangesAsync();

        //     return teams;
        // }
    }
