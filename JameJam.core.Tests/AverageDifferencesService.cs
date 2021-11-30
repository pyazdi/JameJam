using System.Collections.Generic;
using System.Linq;

namespace JameJam.Binance.Core.Tests;

public class AverageDifferencesService
{
  public List<(double difference, int index)> GetDifference( IList<KlinesItem> givenData, IList<KlinesItem> currentRange )
  {
    if ( !givenData.Any() )
    {
      return new List<(double difference, int index)>();
    }

    var windowSize = currentRange.Count;
    var result = new List<(double difference, int index)>(givenData.Count);
    for ( var historyIndex = givenData.Count - 1; historyIndex >= 0; historyIndex-- )
    {
      var index = historyIndex;
      var sumOfDistances = 0.0;
      bool endOfData = false;
      foreach ( var currentItem in currentRange )
      {
        if (index < 0)
        {
          endOfData = true;
          break;
        }
        var historyItem = givenData[index];
        sumOfDistances += currentItem.Average - historyItem.Average;
        index--;
      }

      if (endOfData)
      {
        break;
      }
      result.Add(new(sumOfDistances / windowSize, historyIndex));
    }

    return result;
  }
}