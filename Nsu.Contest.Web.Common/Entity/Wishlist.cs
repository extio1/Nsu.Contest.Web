namespace Nsu.Contest.Web.Common.Entity;

public class Wishlist
{
    public Wishlist() { DesiredEmployees = []; }
    public Wishlist(Guid hackatonId,Employee forEmployee, ICollection<int> desiredEmployees)
    {
        ForEmployee = forEmployee;
        DesiredEmployees = desiredEmployees;
        HackatonId = hackatonId;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid HackatonId { get; set; }
    public Employee ForEmployee { get; set; }
    public int ForEmployeeId { get; set; }
    public ICollection<int> DesiredEmployees { get; set; }
    public string EmployeeType;
};
