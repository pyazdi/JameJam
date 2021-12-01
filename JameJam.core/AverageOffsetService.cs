using System;
using System.Collections.Generic;

namespace JameJam.Binance.Core;

public class AverageOffsetService : IAverageOffsetService
{
  public double GetOffset( IList<KlinesItem> historyData, IList<KlinesItem> currentRange, int historyIndex )
  {
    var windowSize = currentRange.Count;
    var index = historyIndex;
    var sumOfDistances = 0.0;
    foreach ( var currentItem in currentRange )
    {
      if ( index >= historyData.Count )
      {
        throw new IndexOutOfRangeException( $"There is not enough elements after the {historyIndex} item in the history data to calculate a rage of {currentRange.Count} data" );
      }

      var historyItem = historyData[index];
      sumOfDistances += currentItem.Average - historyItem.Average;
      index++;
    }

    return sumOfDistances / windowSize;
  }
}