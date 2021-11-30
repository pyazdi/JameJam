// See https://aka.ms/new-console-template for more information

using JameJam.Binance.Core;

var historyDataFileName = "daily-2017-12--2021-10.csv";
var inputFilePath = GetInputPath( historyDataFileName );

Console.WriteLine($"Loading history data from {inputFilePath}");

var dataLines = File.ReadAllLines(inputFilePath);

Console.WriteLine($"Loaded {dataLines.Length} line of data");

var klinesDataProvider = new KlinesDataProvider();

var numberOfImportedLines = klinesDataProvider.SetData( dataLines );
Console.WriteLine($"imported {dataLines.Length} line of data");


string GetInputPath(string entryName)
{
  var userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
  var outputFolder = Path.Combine(userFolderPath, "BinanceData", entryName);
  return outputFolder;
}