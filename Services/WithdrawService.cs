namespace SimpleRESTServerAPI.Services
{
    public class WithdrawService
    {
        public IEnumerable<Result> FindWithdrawListCombinations(List<int> amounts, List<short> availableDenominations, string currency)
        {
            // Select all combinations of each item in the provided list of values and return
            var results = amounts.Select(_ => new Result
            {
                Amount = _,
                WithdrawCombinations = FindWithdrawCombinations(_, availableDenominations, currency)
            }).ToList();

            return results;
        }

        public IEnumerable<WithdrawCombinations> FindWithdrawCombinations(int remainingValue, List<short> availableDenominations, string currency)
        {
            List<WithdrawCombinations> results = new List<WithdrawCombinations>();
            // Call the method that searches for combinations recursively for the provided value
            FindCombinationsRecursive(remainingValue, availableDenominations, currency, new List<short>(), results, 0);
            return results;
        }

        private void FindCombinationsRecursive(int remainingValue, List<short> availableDenominations, string currency, List<short> currentCombination, List<WithdrawCombinations> results, int index)
        {
            // If it's the last call of this recursive method, make the result and insert into the results list
            if (remainingValue == 0)
            {
                // Obtain the list of denomination combinations
                var denominationCounts = GetDenominationCounts(currentCombination, currency);
                var result = new WithdrawCombinations
                {
                    // Create the description by adding a plus between the denomination count description
                    // Ex: ["3 x 10 EUR", "1 x 100 EUR"] became "3 x 10 EUR + 1 x 100 EUR"
                    Description = string.Join(" + ", denominationCounts.Select(_ => _.Description).ToList()),
                    DenominationCounts = denominationCounts
                };

                // Insert into the result list and return
                results.Add(result);
                return;
            }

            // For each available denomination
            for (int i = index; i < availableDenominations.Count; i++)
            {
                short currentNote = availableDenominations[i];

                // If the remaining value minus the current denomination is greater than zero,
                // we add the current denomination to our current combination and call the
                // recursive method again, but passing the subtracted value
                if (remainingValue - currentNote >= 0)
                {
                    currentCombination.Add(currentNote);
                    FindCombinationsRecursive(remainingValue - currentNote, availableDenominations, currency, currentCombination, results, i);
                    currentCombination.RemoveAt(currentCombination.Count - 1);
                }
            }
        }

        private IEnumerable<DenominationCount> GetDenominationCounts(List<short> combination, string currency)
        {
            // Group the combinations by denomination, count how many times they appear, and generate the description
            var denominationCounts = combination.GroupBy(x => x)
                .Select(group => new DenominationCount
                {
                    Denomination = group.Key,
                    Multiplier = group.Count(),
                    Description = $"{group.Count()} x {group.Key} {currency}"
                })
                .ToList();

            return denominationCounts;
        }

        public class Result
        {
            public required int Amount { get; set; }
            public required IEnumerable<WithdrawCombinations> WithdrawCombinations { get; set; }
        }

        public class WithdrawCombinations
        {
            public required string Description { get; set; }
            public required IEnumerable<DenominationCount> DenominationCounts { get; set; }
        }

        public class DenominationCount
        {
            public required short Denomination { get; set; }
            public required int Multiplier { get; set; }
            public required string Description { get; set; }
        }
    }
}
