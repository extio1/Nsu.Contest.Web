namespace Nsu.Contest.Web.Common.Entity;

public class Wishlist
{
    public Wishlist() { DesiredEmployees = []; }
    public Wishlist(Employee forEmployee, ICollection<int> desiredEmployees)
    {
        ForEmployee = forEmployee;
        DesiredEmployees = desiredEmployees;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public Employee ForEmployee { get; set; }
    public int ForEmployeeId { get; set; }
    public ICollection<int> DesiredEmployees { get; set; }
};
