using System;
using System.IO;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace ExpenseAnalyzer.Core
{
    public class NestBankCsv
    {
        public DateTime DataOperacji { get; set; }
        public string RodzajOperacji { get; set; }
        public decimal Kwota { get; set; }
        public string Waluta { get; set; }
        public string DaneKontrahenta { get; set; }
        public string NumerRachunkuKontrahenta { get; set; }
        public string TytułOperacji { get; set; }
        public DateTime DataKsięgowania { get; set; }
        public decimal SaldoPoOperacji { get; set; }
    }

    class NestBankCsvMapping : CsvMapping<NestBankCsv>
    {
        public NestBankCsvMapping() : base()
        {
            MapProperty(0, x => x.DataOperacji);
            MapProperty(1, x => x.RodzajOperacji);
            MapProperty(2, x => x.Kwota);
            MapProperty(3, x => x.Waluta);
            MapProperty(4, x => x.DaneKontrahenta);
            MapProperty(5, x => x.NumerRachunkuKontrahenta);
            MapProperty(6, x => x.TytułOperacji);
            MapProperty(7, x => x.DataKsięgowania);
            MapProperty(8, x => x.SaldoPoOperacji);
        }
    }


    public class CsvTransactionParser : ITransactionParser
    {
        public ImportedTransactions Parse(Stream inputFileStream)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            var csvParser = new CsvParser<NestBankCsv>(csvParserOptions, new NestBankCsvMapping());

            var records = csvParser.ReadFromStream(inputFileStream, Encoding.UTF8);

            // TODO: skipped
            var transactions =
                records
                .Where(record => record.IsValid)
                .Select(record => new Transaction(record.Result.Kwota, record.Result.DataOperacji));

            return new ImportedTransactions(transactions);
        }
    }
}