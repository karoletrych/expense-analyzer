using System;
using Xunit;
using ExpenseAnalyzer.Core;
using System.IO;
using FluentAssertions;

namespace Core.Tests
{
    public class NestBankCsvParserTests
    {
        [Fact]
        public void ParsesCsv()
        {
            using(FileStream fs = File.OpenRead("../../../lista.csv"))
            {
                var transactions = new NestBankCsvTransactionParser().Parse(fs);
                transactions.Transactions.Should().NotBeEmpty();
            }
        }
    }
}
