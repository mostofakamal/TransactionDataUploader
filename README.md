# Transaction Data Uploader

A service to upload transaction data from files of various formats into database and query transactions by specified criteria.

The project is implemented with following tools, Technologies and third party class libraries

- .NET Core 3.1
- TinyCsvParser for parsing csv files
- Fluent Validation for data model validation
- EntityFramework Core as ORM with Code first Migration
- Xunit for unit testing
- Moq as mocking framework in Unit tests

## Pre-requisites :

- .NET Core 3.1 SDK
- Microsoft SQL server 2016

## How to run the application:

The Web project contains a setting file named `appsettings.json` , there is a DbConnection string named `DefaultConnection` . Change the server name with valid server name . The project uses EF Core Code first migration. Therefore project will create the database schemas when it loads up , So no need to run the Scripts manually. However the migration related DbScripts are provided in the `DbScripts` folder. To run the web app -> Go to the root directory of TransactionDataUploader.Web and run -> `dotnet run`

## Project Description:

- TransactionDataUploader.Core :
  This is the .net Core class library project where all the necessary domain and infrastructural concerns e.g. reading files, saving to database etc are implemented. It uses a third party library named TinyCsvParser for parsing the CSV file. The main file parsing logic is inside the abstract class `FileParser` which has one implementation for CSV file named `CsvFileParser` and another one named `XmlFileParser` for xml file. The `FileParser` abstract class uses template method Design pattern to abstract different steps , e.g. Parsing, Validation, mapping etc.
  Used EfCore as an ORM with Code first migration. Fluent validation is used with different validation rules.

- TransactionDataUploader.Web :
  This is ASP.NET Core MVC web app which handles Uploading transaction data files and displaying transactions. There is a TransactionsController api Controller which exposes a get endpoint on route -> `api/transactions` which supports different query parameters for filtering data

- TransactionDataUploader.Tests :
  This is a Xunit project which contains the unit tests of relevant class specific methods.
