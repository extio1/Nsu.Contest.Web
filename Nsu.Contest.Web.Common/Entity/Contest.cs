namespace Nsu.Contest.Web.Common.Entity;

public class Contest {

    public Contest(Guid id, double score)
    {
        Id = id;
        Score = score;
    }
    public Guid Id { get; set; } = Guid.NewGuid();
    public double Score { get; set; }

};
