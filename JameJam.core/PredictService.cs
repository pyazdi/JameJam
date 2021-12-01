using System;
using System.Collections.Generic;
using System.Linq;
using JameJam.Binance.Core.Tests;

namespace JameJam.Binance.Core;

public class PredictService
{
  private MatchDataService _matchDataService;

  public PredictService( MatchDataService matchDataService )
  {
    _matchDataService = matchDataService;
  }

  public List<(double matchFactor, double percent)> Predict( IList<KlinesItem> historyData, int matchPatternSize, int predictionsCount )
  {
    var matches = _matchDataService.GetMatchFactor( historyData, historyData.TakeLast( matchPatternSize ).ToList() );

    IEnumerable<(double matchFactor, int index)> bestMatches = matches.OrderBy( item => item.matchFactor ).Take( predictionsCount + 1 );

    var result = new List<(double, double)>();
    foreach ( var bestMatch in bestMatches )
    {
      if ( bestMatch.matchFactor == 0 && bestMatch.index == historyData.Count - matchPatternSize )
      {
        continue;
      }

      var nextDayIndex = bestMatch.index + matchPatternSize;

      result.Add( new ValueTuple<double, double> ( bestMatch.matchFactor, historyData[nextDayIndex].Percent) );

      //Console.WriteLine($"Factor: {bestMatch.matchFactor:F0} for " +
      //                  $" {historyData.Last().OpenTime.AddDays(1).ToShortDateString()} ->" +
      //                  $" {historyData[nextDayIndex].OpenTime.ToShortDateString()} is " +
      //                  $" {(difference > 0 ? '+' : '-')} {Math.Abs(difference):C} ({percent:P1}) ");
    }

    return result;
  }
}