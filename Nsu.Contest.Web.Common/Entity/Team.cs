namespace Nsu.Contest.Web.Common.Entity;

public class Team
{  
    public Team() { }
    public Team (Teamlead teamlead, Junior junior)
    {
        Teamlead = teamlead;
        Junior = junior;
    }
    public Guid Id { get; set; } = Guid.NewGuid();
    public Teamlead Teamlead { get; set; }
    public Junior Junior { get; set; }
}
