using System;
using System.Collections.Generic;

namespace ExpenseAnalyzer.Core
{
    public class ImportedTransactions
    {
        public IEnumerable<Transaction> Transactions { get; }

        public ImportedTransactions(IEnumerable<Transaction> transactions)
        {
            Transactions = transactions;
        }
    }
}
