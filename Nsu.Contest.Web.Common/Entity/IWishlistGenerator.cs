namespace Nsu.Contest.Web.Common.Entity;

public interface IWishlistGenerator
{
    IEnumerable<Wishlist> GenerateWishlists(IEnumerable<Employee> forEmpls, IEnumerable<Employee> ofEmpls);
}
