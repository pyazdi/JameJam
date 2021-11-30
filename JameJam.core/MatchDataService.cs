﻿using System.Collections.Generic;
using System.Linq;

namespace JameJam.Binance.Core.Tests;

public class MatchDataService
{
  private IAverageOffsetService _averageOffsetService;
  private readonly IMatchCalculatorService _matchCalculatorService;

  public MatchDataService( IAverageOffsetService averageOffsetService, IMatchCalculatorService matchCalculatorService )
  {
    _averageOffsetService = averageOffsetService;
    _matchCalculatorService = matchCalculatorService;
  }

  public List<(double matchFactor, int index)> GetMatchFactor( IList<KlinesItem> historyData, IList<KlinesItem> currentRange )
  {
    if (!historyData.Any())
    {
      return new List<(double difference, int index)>();
    }

    var result = new List<(double difference, int index)>(historyData.Count);
    for (var historyIndex = historyData.Count - 1; historyIndex >= currentRange.Count - 1; historyIndex--)
    {
      var offset = _averageOffsetService.GetOffset(historyData, currentRange, historyIndex);
      var matchFactor = _matchCalculatorService.GetMatchFactor( historyData, currentRange, historyIndex, offset );

      result.Add(new(matchFactor, historyIndex));
    }

    return result;
  }

}