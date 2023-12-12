
using System.ComponentModel.DataAnnotations;
using static SimpleRESTServerAPI.Services.CustomerService;

namespace SimpleRESTServerAPI.Services
{
    // Custom DataAnnotation Attribute Validator to check if the Id already exists
    public class UniqueIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get the Id who has the attribute [UniqueId]
            var id = (int)value;

            // Get the list of current customers
            List<Customer> customerList = new CustomerService().ListCustomers();
            var a = 0;
            // If the ID exists, return the validation error
            if (customerList.Any(_ => _.Id == id))
                return new ValidationResult($"The Id {id} already exists in the list of customers.", new[] { validationContext.MemberName });

            return ValidationResult.Success;
        }
    }
}
