namespace Nsu.Contest.Web.Common.Entity;

public class Team
{  
    public Team() { }
    public Team (Guid hackatonId, Teamlead teamlead, Junior junior)
    {
        Teamlead = teamlead;
        Junior = junior;
        HackatonId = hackatonId;
    }
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid HackatonId { get; set; } = Guid.NewGuid();

    public Teamlead Teamlead { get; set; }
    public Junior Junior { get; set; }
}
