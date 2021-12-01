// See https://aka.ms/new-console-template for more information

using JameJam.Binance.Core;
using JameJam.Binance.Core.Tests;

var historyDataFileName = "daily-2017-12--2021-11.csv";
var inputFilePath = GetInputPath( historyDataFileName );

Console.WriteLine( $"Loading history data from {inputFilePath}" );

var dataLines = File.ReadAllLines( inputFilePath );

Console.WriteLine( $"Loaded {dataLines.Length} line of data" );

var klinesDataService = new KlinesDataService();
var historyData = klinesDataService.GetKlines(dataLines);

Console.WriteLine($"imported {historyData.Count} line of data");
Console.WriteLine($"Last data {historyData.Last()}");
Console.WriteLine(  );
Console.WriteLine(  );
Console.WriteLine( "Enter a task number and press enter" );
Console.WriteLine( "1. Predict the next day" );
Console.WriteLine( "2. Test prediction for the last 100 days" );

var taskId = Console.ReadLine();

var matchPatternSize = 15;
var predictionsCount = 10;

var predictService = new PredictService( new MatchDataService( new AverageOffsetService(), new MatchCalculatorService() ) );
if ( taskId.StartsWith( "1" ) )
{
  var predictions = predictService.Predict(historyData, matchPatternSize, predictionsCount);
  foreach (var prediction in predictions)
  {
    Console.WriteLine($"Factor: {prediction.matchFactor:F0} " +
                      $"With %{prediction.percent * 100:F1}");
  }
}
else if ( taskId.StartsWith( "2" ) )
{
  for (int i = 1; i < 100; i++)
  {
    var virtualCurrentDayIndex = historyData.Count - i;
    var predictions = predictService.Predict(historyData.Take(virtualCurrentDayIndex).ToList(), matchPatternSize, 1);
    foreach (var prediction in predictions)
    {
      Console.WriteLine($"{historyData[virtualCurrentDayIndex].OpenTime:d} - F:{prediction.matchFactor:F0} - " +
                        $"Prediction %{prediction.percent*100:F1} ->" +
                        $"Actual %{historyData[virtualCurrentDayIndex].Percent * 100:F1}");
    }
  }
}


string GetInputPath( string entryName )
{
  var userFolderPath = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
  var outputFolder = Path.Combine( userFolderPath, "JameJam", entryName );
  return outputFolder;
}