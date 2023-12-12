using static SimpleRESTServerAPI.Services.CustomerService;

namespace SimpleRESTServerAPI.Services
{
    public class CustomerIndexFinderService
    {
        public class CustomerComparer : IComparer<Customer>
        {
            public int Compare(Customer x, Customer y)
            {
                // Compare based on LastName
                int result = string.Compare(x.LastName, y.LastName, StringComparison.Ordinal);

                if (result == 0)
                {
                    // If LastName is the same, compare based on FirstName
                    result = string.Compare(x.FirstName, y.FirstName, StringComparison.Ordinal);
                }

                return result;
            }
        }

        public int BinarySearch(List<Customer> list, Customer item)
        {
            int low = 0;
            int high = list.Count - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                int comparison = new CustomerComparer().Compare(list[mid], item);

                if (comparison == 0)
                {
                    // Found an exact match
                    return mid;
                }
                else if (comparison < 0)
                {
                    // Item is on the right
                    low = mid + 1;
                }
                else
                {
                    // Item is on the left
                    high = mid - 1;
                }
            }

            // Return the position where the item should be
            return low;
        }
    }
}
