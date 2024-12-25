namespace Nsu.Contest.Web.Common.Entity;

public interface IWishlistGenerator
{
    IEnumerable<Wishlist> GenerateWishlists(Guid hackatonId, IEnumerable<Employee> forEmpls, IEnumerable<Employee> ofEmpls);
    Wishlist GenerateWishlist(Guid hackatonId, Employee forEmpl, IEnumerable<Employee> ofEmpls);
}
