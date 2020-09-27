using System.IO;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.Core
{
    public interface ITransactionParser
    {
        Task<ImportedTransactions> Parse(Stream inputFileStream); 
    }
}