using FluentAssertions;
using TransactionDataUploader.Core.Domain.Services;
using Xunit;

namespace TransactionDataUploader.Tests
{
    public class CsvFileParserTests
    {
        private readonly CsvFileParser _csvFileParser;
        public CsvFileParserTests()
        {
            _csvFileParser = new CsvFileParser();
        }

        [Fact]
        public void Test_ParseCSVFile_FileContentNotValid_ReturnErrors()
        {
            var invalidFileContent = "Some invalid file content";
            var result = _csvFileParser.ExtractDataFromContent(invalidFileContent);
            result.HasError.Should().BeTrue();
            result.Errors[0].Should().Be("Data parsing failed");
        }


        [Fact]
        public void Test_ParseCSVFile_SomeFieldHasInvalidDateData_ReturnErrors()
        {
            var fileContent = "\"Invoice0000001\",\"InvalidAmount\", \"USD\", \"20/02/2019 12:33:16\", \"Approved\"";
            var result = _csvFileParser.ExtractDataFromContent(fileContent);
            result.HasError.Should().BeTrue();
        }

        [Fact]
        public void Test_ParseCSVFile_SomeFieldHasTransactionIdToLongData_ReturnErrors()
        {
            const string csvFileContent = "\"Invoice000000100000000000000000000000000000000000000000000000000000\",\"200\", \"USD\", \"20/02/2019 12:33:16\", \"Approved\"";
            var result = _csvFileParser.ExtractDataFromContent(csvFileContent);
            result.HasError.Should().BeTrue();
        }

        [Fact]
        public void Test_ParseCSVFile_SomeFieldHasInvalidTransactionStatus_ReturnErrors()
        {
            const string fileContent = "\"Invoice00000010\",\"200\", \"USD\", \"20/02/2019 12:33:16\", \"InvalidStatus\"";
            var result = _csvFileParser.ExtractDataFromContent(fileContent);
            result.HasError.Should().BeTrue();
        }

        [Fact]
        public void Test_ParseCSVFile_AllValidFields_ShouldReturnResultWithNoErrors()
        {
            const string fileContent = "\"Inv000011\",\"200\", \"USD\", \"20/02/2019 12:33:16\", \"Approved\"";
            var result = _csvFileParser.ExtractDataFromContent(fileContent);
            result.HasError.Should().BeFalse();
            result.Data.Count.Should().Be(1);
            result.Data[0].TransactionId.Should().Be("Inv000011");
            result.Data[0].Amount.Should().Be(200);
        }


    }
}