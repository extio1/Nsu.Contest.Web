namespace Nsu.Contest.Web.Common.Entity;

using Nsu.Contest.Web.Common.Util;

public class WishlistGenerator : IWishlistGenerator
{
    public WishlistGenerator()
    {}

    public IEnumerable<Wishlist> GenerateWishlists(Guid hackatonId, IEnumerable<Employee> forEmpls, IEnumerable<Employee> ofEmpls)
    {
        if(forEmpls.Count() != ofEmpls.Count())
        {
            throw new ArgumentException("All collections must be the same length.");
        }
        
        var employeesCount = forEmpls.Count();
        var wishlists = new List<Wishlist>(employeesCount);

        foreach (var forEmpl in forEmpls)
        {
            var prioritiesForEmpl = RandomGenerator.GeneratePermutation(employeesCount);
            wishlists.Add
            (
                new Wishlist
                (
                    hackatonId,
                    forEmpl, 
                    prioritiesForEmpl.Select(ind => ofEmpls.ElementAt(ind-1)).ToArray().Select(e => e.Id).ToArray()
                )
            );
        }

        return wishlists;
    }

    public static Wishlist GenerateWishlist(Guid hackatonId, Employee forEmpl, IEnumerable<Employee> ofEmpls)
    {
        var prioritiesForEmpl = RandomGenerator.GeneratePermutation(ofEmpls.Count());
        return new Wishlist(hackatonId, forEmpl, prioritiesForEmpl);
    }
}
