using System;
using System.Collections.Generic;

namespace JameJam.Binance.Core;

public class MatchCalculatorService : IMatchCalculatorService
{
  public double GetMatchFactor(   IList<KlinesItem> historyData, IList<KlinesItem> currentRange, int historyIndex, double offset )
  {
    var index = historyIndex;
    var sumOfDifferences = 0.0;
    foreach ( var currentItem in currentRange )
    {
      if ( index >= historyData.Count )
      {
        throw new IndexOutOfRangeException( $"There is not enough elements after the {historyIndex} item in the history data to calculate a rage of {currentRange.Count} data" );
      }

      var historyItem = historyData[index];
      sumOfDifferences += Math.Abs( currentItem.Average - offset - historyItem.Average );
      index++;
    }

    return sumOfDifferences;
  }
}