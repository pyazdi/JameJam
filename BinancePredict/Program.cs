// See https://aka.ms/new-console-template for more information

using JameJam.Binance.Core;
using JameJam.Binance.Core.Tests;

var historyDataFileName = "daily-2017-12--2021-11.csv";
var inputFilePath = GetInputPath( historyDataFileName );

Console.WriteLine($"Loading history data from {inputFilePath}");

var dataLines = File.ReadAllLines(inputFilePath);

Console.WriteLine($"Loaded {dataLines.Length} line of data");

var klinesDataProvider = new KlinesDataProvider();

var numberOfImportedLines = klinesDataProvider.SetData( dataLines );
Console.WriteLine($"imported {dataLines.Length} line of data");

var matchDataService = new MatchDataService( new AverageOffsetService(), new MatchCalculatorService() );
var matchPatternSize = 15;
var historyData = klinesDataProvider.Container;
var matches = matchDataService.GetMatchFactor( historyData, historyData.TakeLast( matchPatternSize ).ToList() );

File.WriteAllLines( @"D:\temp\bb.txt", matches.Select( item => $"{item.matchFactor:F2},{item.index}" ) );

IEnumerable<(double matchFactor, int index)> bestMatches = matches.OrderBy( item => item.matchFactor ).Take( 10 );
foreach ( var bestMatch in bestMatches)
{
  if ( bestMatch.matchFactor == 0 && bestMatch.index == historyData.Count-matchPatternSize )
  {
    continue;
  }
  var nextDayIndex = bestMatch.index + matchPatternSize;
  var difference = historyData[nextDayIndex].High - historyData[nextDayIndex].Low;
  var percent = difference / historyData[nextDayIndex].Low;
  Console.WriteLine($"Factor: {bestMatch.matchFactor:F0} for " +
                    $" {historyData.Last().OpenTime.AddDays( 1 ).ToShortDateString()} ->" +
                    $" {historyData[nextDayIndex].OpenTime.ToShortDateString()} is " +
                    $" {(difference>0?'+':'-')} {Math.Abs(difference):C} ({percent:P1}) ");
}


string GetInputPath(string entryName)
{
  var userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
  var outputFolder = Path.Combine(userFolderPath, "JameJam", entryName);
  return outputFolder;
}