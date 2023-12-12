using Microsoft.AspNetCore.Mvc;
using SimpleRESTServerAPI.Services;

namespace SimpleRESTServerAPI.Controllers
{
    [ApiController]
    [Route("api/atm-withdraw")]
    public class ATMWithdrawController : ControllerBase
    {
        public ATMWithdrawController()
        {
        }

        [HttpGet(Name = "GetWithdrawOptions")]
        public IEnumerable<WithdrawService.Result> Get(int? amount)
        {
            // List of available denominations in the ATM 
            List<short> availableDenominations = new List<short> { 10, 50, 100 };
            // Denominations currency
            string currency = "EUR";

            // If the endpoint receives any parameters, use them; otherwise, use the example list of values
            List<int> amounts = amount.HasValue ?
                new List<int> { amount.Value } :
                new List<int> { 30, 50, 60, 80, 140, 230, 370, 610, 980 };

            // Call the funcion that find all the combinations for each value passed and return
            return new WithdrawService().FindWithdrawListCombinations(amounts, availableDenominations, currency);
        }
    }
}
