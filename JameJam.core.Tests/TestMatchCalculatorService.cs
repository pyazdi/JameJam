using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace JameJam.Binance.Core.Tests;

[TestFixture]
public class TestMatchCalculatorService
{
  [Test]
  public void GivenSameDataOffsetZero_WhenCalculatesMatchValue_ThenZero()
  {
    MatchCalculatorService service = new MatchCalculatorService();
    var givenData = new List<KlinesItem>
    {
      new () {High = 1.2, Low = 1.1}, // average 1.15
    };

    var currentRange = new List<KlinesItem>()
    {
      new () {High = 1.2, Low = 1.1}, // average 1.15
    };

    // Action
    var result = service.GetMatchFactor( givenData, currentRange, 0, 0 );

    // Assert
    result.Should().Be( 0 );
  }

  [Test]
  public void GivenSameDataWithOffset_WhenCalculatesMatchValue_ThenZero()
  {
    MatchCalculatorService service = new MatchCalculatorService();
    var givenData = new List<KlinesItem>
    {
      new () {High = 1.2, Low = 1.1},
    };

    var currentRange = new List<KlinesItem>()
    {
      new () {High = 1.2 + 5, Low = 1.1 + 5},
    };

    // Action
    var result = service.GetMatchFactor( givenData, currentRange, 0, 5 );

    // Assert
    result.Should().BeApproximately( 0.0, 0.0001 );
  }

  [Test]
  public void GivenOneDifferenceDataWithOffset_WhenCalculatesMatchValue_ThenCorrectMatchFactor()
  {
    MatchCalculatorService service = new MatchCalculatorService();
    var givenData = new List<KlinesItem>
    {
      new () {High = 2, Low = 1},
    };

    var currentRange = new List<KlinesItem>()
    {
      new () {High = 2 + 5, Low = 0.5 + 5},
    };

    // Action
    var result = service.GetMatchFactor( givenData, currentRange, 0, 5 );

    // Assert
    result.Should().BeApproximately( 0.5, 0.0001 );
  }

  [Test]
  public void GivenOneDifferenceDataWithOffset1_WhenCalculatesMatchValue_ThenCorrectMatchFactor()
  {
    MatchCalculatorService service = new MatchCalculatorService();
    var givenData = new List<KlinesItem>
    {
      new () {High = 5, Low = 3},
    };

    var currentRange = new List<KlinesItem>()
    {
      new () {High = 1 + 5, Low = 3 + 5},
    };

    // Action
    var result = service.GetMatchFactor( givenData, currentRange, 0, 5 );

    // Assert
    result.Should().BeApproximately( 4, 0.0001 );
  }

  [Test]
  public void GivenDifferenceDataWithOffset_WhenCalculatesMatchValue_ThenCorrectMatchFactor()
  {
    MatchCalculatorService service = new MatchCalculatorService();
    var givenData = new List<KlinesItem>
    {
      new () {High = 5, Low = 3},
      new () {High = 6, Low = 4},
    };

    var currentRange = new List<KlinesItem>()
    {
      new () {High = 5 + 5, Low = 3 + 5},
      new () {High = 4 + 5, Low = 3 + 5},
    };

    // Action
    var result = service.GetMatchFactor( givenData, currentRange, 1, 5 );

    // Assert
    result.Should().BeApproximately( 3, 0.0001 );
  }
}