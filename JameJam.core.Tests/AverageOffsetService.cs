using System;
using System.Collections.Generic;
using System.Linq;

namespace JameJam.Binance.Core.Tests;

public class AverageOffsetService
{
  public double GetOffset( IList<KlinesItem> givenData, IList<KlinesItem> currentRange, int historyIndex )
  {
    var windowSize = currentRange.Count;
    var index = historyIndex;
    var sumOfDistances = 0.0;
    foreach ( var currentItem in currentRange )
    {
      if ( index < 0 )
      {
        throw new IndexOutOfRangeException( $"There is not enough elements after the {historyIndex} item in the history data to calculate a rage of {currentRange.Count} data" );
      }

      var historyItem = givenData[index];
      sumOfDistances += currentItem.Average - historyItem.Average;
      index--;
    }

    return sumOfDistances / windowSize;
  }
}