using FluentAssertions;
using TransactionDataUploader.Core.Domain.Services;
using Xunit;

namespace TransactionDataUploader.Tests
{
    public class XmlFileParserTests
    {
        private readonly XmlFileParser _xmlFileParser;
        public XmlFileParserTests()
        {
            _xmlFileParser = new XmlFileParser();
        }


        [Fact]
        public void Test_ParseXMLFile_FileContentNotValid_ReturnErrors()
        {
            var xmlFileContent = "Some invalid file content";
            var result= _xmlFileParser.ExtractDataFromContent(xmlFileContent);
            result.HasError.Should().BeTrue();
            result.Errors[0].Should().Be("Data parsing failed");
        }

        [Fact]
        public void Test_ParseXMLFile_SomeFieldHasInvalidDateData_ReturnErrors()
        {
            var xmlFileContent = @"<Transactions><Transaction id='Inv00001'><TransactionDate>2020/12/02</TransactionDate><PaymentDetails><Amount>200.00</Amount><CurrencyCode>USD</CurrencyCode></PaymentDetails><Status>Done</Status></Transaction><Transaction id = 'Inv00002'><TransactionDate>2019-01-24T16:09:15</TransactionDate><PaymentDetails><Amount> 10000.00 </Amount><CurrencyCode>EUR</CurrencyCode></PaymentDetails><Status>Rejected</Status></Transaction></Transactions>"; ;
            var result = _xmlFileParser.ExtractDataFromContent(xmlFileContent);
            result.HasError.Should().BeTrue();
        }

        [Fact]
        public void Test_ParseXMLFile_SomeFieldHasInvalidAmountData_ReturnErrors()
        {
            var xmlFileContent = @"<Transactions><Transaction id='Inv00001'><TransactionDate>2019-01-23T13:45:10</TransactionDate><PaymentDetails><Amount>InvalidAmount</Amount><CurrencyCode>USD</CurrencyCode></PaymentDetails><Status>Done</Status></Transaction><Transaction id = 'Inv00002'><TransactionDate>2019-01-24T16:09:15</TransactionDate><PaymentDetails><Amount> 10000.00 </Amount><CurrencyCode>EUR</CurrencyCode></PaymentDetails><Status>Rejected</Status></Transaction></Transactions>"; ;
            var result = _xmlFileParser.ExtractDataFromContent(xmlFileContent);
            result.HasError.Should().BeTrue();
        }

        [Fact]
        public void Test_ParseXMLFile_SomeFieldHasTransactionIdToLongData_ReturnErrors()
        {
            var xmlFileContent = @"<Transactions><Transaction id='Inv0000112525252352266666666666666666663663366355222222222222222'><TransactionDate>2019-01-23T13:45:10</TransactionDate><PaymentDetails><Amount>200.00</Amount><CurrencyCode>USD</CurrencyCode></PaymentDetails><Status>Done</Status></Transaction><Transaction id = 'Inv00002'><TransactionDate>2019-01-24T16:09:15</TransactionDate><PaymentDetails><Amount> 10000.00 </Amount><CurrencyCode>EUR</CurrencyCode></PaymentDetails><Status>Rejected</Status></Transaction></Transactions>"; ;
            var result = _xmlFileParser.ExtractDataFromContent(xmlFileContent);
            result.HasError.Should().BeTrue();
        }

        [Fact]
        public void Test_ParseXMLFile_SomeFieldHasInvalidTransactionStatus_ReturnErrors()
        {
            var xmlFileContent = @"<Transactions><Transaction id='Inv000011'><TransactionDate>2019-01-23T13:45:10</TransactionDate><PaymentDetails><Amount>200.00</Amount><CurrencyCode>USD</CurrencyCode></PaymentDetails><Status>InvalidStatus</Status></Transaction><Transaction id = 'Inv00002'><TransactionDate>2019-01-24T16:09:15</TransactionDate><PaymentDetails><Amount> 10000.00 </Amount><CurrencyCode>EUR</CurrencyCode></PaymentDetails><Status>Rejected</Status></Transaction></Transactions>"; ;
            var result = _xmlFileParser.ExtractDataFromContent(xmlFileContent);
            result.HasError.Should().BeTrue();
        }

        [Fact]
        public void Test_ParseXMLFile_SomeFieldHasAllValidFields_ShouldReturnResultWithNoErrors()
        {
            var xmlFileContent = @"<Transactions><Transaction id='Inv000011'><TransactionDate>2019-01-23T13:45:10</TransactionDate><PaymentDetails><Amount>200.00</Amount><CurrencyCode>USD</CurrencyCode></PaymentDetails><Status>Done</Status></Transaction><Transaction id = 'Inv00002'><TransactionDate>2019-01-24T16:09:15</TransactionDate><PaymentDetails><Amount> 10000.00 </Amount><CurrencyCode>EUR</CurrencyCode></PaymentDetails><Status>Rejected</Status></Transaction></Transactions>"; ;
            var result = _xmlFileParser.ExtractDataFromContent(xmlFileContent);
            result.HasError.Should().BeFalse();
            result.Data.Count.Should().Be(2);
            result.Data[0].TransactionId.Should().Be("Inv000011");
            result.Data[1].TransactionId.Should().Be("Inv00002");
            result.Data[0].Amount.Should().Be(200);
            result.Data[1].Amount.Should().Be(10000);
        }
    }
}
