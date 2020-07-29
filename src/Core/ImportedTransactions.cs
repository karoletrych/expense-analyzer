using System;

namespace ExpenseAnalyzer.Core
{
    public class ImportedTransactions
    {
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
