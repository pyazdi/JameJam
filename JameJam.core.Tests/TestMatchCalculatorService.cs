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
      new () {Open = 1.2, Close = 1.1}, // average 1.15
    };

    var currentRange = new List<KlinesItem>()
    {
      new () {Open = 1.2, Close = 1.1}, // average 1.15
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
      new () {Open = 1.2, Close = 1.1},
    };

    var currentRange = new List<KlinesItem>()
    {
      new () {Open = 1.2 + 5, Close = 1.1 + 5},
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
      new () {Open = 2, Close = 1},
    };

    var currentRange = new List<KlinesItem>()
    {
      new () {Open = 2 + 5, Close = 0.5 + 5},
    };

    // Action
    var result = service.GetMatchFactor( givenData, currentRange, 0, 5 );

    // Assert
    result.Should().BeApproximately( (2+1)/2.0 - (2+0.5)/2.0, 0.0001 );
  }

  [Test]
  public void GivenOneDifferenceDataWithOffset1_WhenCalculatesMatchValue_ThenCorrectMatchFactor()
  {
    MatchCalculatorService service = new MatchCalculatorService();
    var givenData = new List<KlinesItem>
    {
      new () {Open = 5, Close = 3},
    };

    var currentRange = new List<KlinesItem>()
    {
      new () {Open = 1 + 5, Close = 3 + 5},
    };

    // Action
    var result = service.GetMatchFactor( givenData, currentRange, 0, 5 );

    // Assert
    result.Should().BeApproximately( (5 +3)/2 - (1+3)/2, 0.0001 );
  }

  [Test]
  public void GivenDifferenceDataWithOffset_WhenCalculatesMatchValue_ThenCorrectMatchFactor()
  {
    MatchCalculatorService service = new MatchCalculatorService();
    var givenData = new List<KlinesItem>
    {
      new () {Open = 5, Close = 3}, // average 4
      new () {Open = 6, Close = 4}, // average 5
    };

    var currentRange = new List<KlinesItem>()
    {
      new () {Open = 5 + 5, Close = 3 + 5}, // average without offset 4
      new () {Open = 4 + 5, Close = 3 + 5}, // average without offset 3.5
    };

    // Action
    var result = service.GetMatchFactor( givenData, currentRange, 0, 5 );

    // Assert
    result.Should().BeApproximately( (4-4) + (5-3.5), 0.0001 );
  }
}