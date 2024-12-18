namespace Nsu.Contest.Web.HRManager.Clients;

using Nsu.Contest.Web.HRDirector.Controllers;

using Refit;

public class IHRDirectorClient
{
    [Post("/api/submit")]
    Task SubmitData([Body] HRDirectorRequest request);
}
