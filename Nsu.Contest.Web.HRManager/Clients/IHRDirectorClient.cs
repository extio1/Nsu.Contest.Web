namespace Nsu.Contest.Web.HRManager.Clients;

using Nsu.Contest.Web.HRDirector.Controllers;

using Refit;

public interface IHRDirectorClient
{
    [Post("/api/submit")]
    Task SubmitData([Body] HRDirectorRequest request);
}
