namespace Nsu.Contest.Web.HRDirector.Model;

public interface ITeamEstimatingStrategy 
{
    public double Calculate(IEnumerable<double> inputSequence);
}
