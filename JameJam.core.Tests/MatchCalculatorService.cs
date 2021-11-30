using System;
using System.Collections.Generic;

namespace JameJam.Binance.Core.Tests;

public class MatchCalculatorService
{
  public double GetDifferenceFactor( List<KlinesItem> givenData, List<KlinesItem> currentRange, int historyIndex, double offset )
  {
    var index = historyIndex;
    var sumOfDifferences = 0.0;
    foreach (var currentItem in currentRange)
    {
      if (index < 0)
      {
        throw new IndexOutOfRangeException($"There is not enough elements after the {historyIndex} item in the history data to calculate a rage of {currentRange.Count} data");
      }

      var historyItem = givenData[index];
      sumOfDifferences += Math.Abs(currentItem.High - offset - historyItem.High);
      sumOfDifferences += Math.Abs(currentItem.Low - offset - historyItem.Low);
      index--;
    }

    return sumOfDifferences;
  }
}