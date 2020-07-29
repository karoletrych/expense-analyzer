using System.IO;

namespace ExpenseAnalyzer.Core
{
    public interface ITransactionParser
    {
        ImportedTransactions Parse(Stream inputFileStream); 
    }
}