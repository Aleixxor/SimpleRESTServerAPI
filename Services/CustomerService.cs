using System.ComponentModel.DataAnnotations;

namespace SimpleRESTServerAPI.Services
{
    public class CustomerService
    {
        private readonly DataRepositoryAccessService<List<Customer>> _dataRepositoryAccessService;

        // JSON path
        private readonly static string _dataFilePath = "Data/Customers.json";

        public CustomerService()
        {
            _dataRepositoryAccessService = new DataRepositoryAccessService<List<Customer>>();
        }

        public List<Customer> ListCustomers()
        {
            // Search the customers in the JSON file and return
            List<Customer> customers = _dataRepositoryAccessService.GetData(_dataFilePath) ?? [];
            return customers;
        }

        public void InsertCustomer(List<Customer> newCustomers)
        {
            // Get the 
            List<Customer> customers = ListCustomers();

            CustomerIndexFinderService customerIndexFinderService = new();

            foreach (var newCustomer in newCustomers)
            {
                // Do a binary search, to get the index where the new customer must be inserted
                int index = customerIndexFinderService.BinarySearch(customers, newCustomer);
                customers.Insert(index, newCustomer);
            }

            // Save the new customer list on the JSON file
            _dataRepositoryAccessService.SaveData(customers, _dataFilePath);
        }

        public class Customer
        {
            [Required]
            [UniqueId] // Custom Data Annotion Attribute created to search for Id duplications
            public required int Id { get; set; }

            [Required]
            public required string FirstName { get; set; }

            [Required]
            public required string LastName { get; set; }

            [Required]
            [Range(18, int.MaxValue)]
            public required short Age { get; set; }
        }
    }
}
