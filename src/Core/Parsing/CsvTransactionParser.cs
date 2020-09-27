using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace ExpenseAnalyzer.Core
{
    public class NestBankCsv
    {
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ContractorData { get; set; }
        public string ContractorAccountNumber { get; set; }
        public string TransactionTitle { get; set; }
        public DateTime PostingDate { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
    }

    class NestBankCsvMapping : CsvMapping<NestBankCsv>
    {
        public NestBankCsvMapping()
        {
            MapProperty(0, x => x.TransactionDate);
            MapProperty(1, x => x.PostingDate);
            MapProperty(2, x => x.TransactionType);
            MapProperty(3, x => x.Amount);
            MapProperty(4, x => x.Currency);
            MapProperty(5, x => x.ContractorData);
            MapProperty(6, x => x.ContractorAccountNumber);
            MapProperty(7, x => x.TransactionTitle);
            MapProperty(8, x => x.BalanceAfterTransaction);
        }
    }


    public class NestBankCsvTransactionParser : ITransactionParser
    {
        public Task<ImportedTransactions> Parse(Stream inputFileStream)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            var csvParser = new CsvParser<NestBankCsv>(csvParserOptions, new NestBankCsvMapping());

            var records = csvParser.ReadFromStream(inputFileStream, Encoding.UTF8);

            var transactions =
                records
                .Where(record => record.IsValid)
                .Select(record => new Transaction(record.Result.Amount, record.Result.TransactionDate));

            return Task.FromResult(new ImportedTransactions(transactions));
        }
    }
}