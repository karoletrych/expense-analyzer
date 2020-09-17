using System;
using TinyCsvParser.Mapping;

namespace ExpenseAnalyzer.Core
{
    public class Transaction
    {
        public Transaction(decimal amount, DateTime date)
        {
            Amount = amount;
            Date = date;
        }

        public decimal Amount { get; }
        public DateTime Date { get; }
    }
}
