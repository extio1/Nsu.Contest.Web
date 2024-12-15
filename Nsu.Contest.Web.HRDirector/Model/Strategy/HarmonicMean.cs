namespace Nsu.Contest.Web.HRDirector.Model.Strategy;

public class HarmonicMean : ITeamEstimatingStrategy
{
    public double Calculate(IEnumerable<double> inputSequence) {
        double sumReciprocals = 0.0;
        int c = 0;

        foreach (var num in inputSequence)
        {
            if(num <= 0) {
                throw new ArgumentException($"All elements of inputSequence must be positive, but {num} found");
            }
            sumReciprocals += 1 / num;
            Console.WriteLine(c);
            c++;
        }

        return inputSequence.Count() / sumReciprocals;
    }
}
