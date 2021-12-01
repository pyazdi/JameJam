using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace JameJam.Binance.Core.Tests;

[TestFixture]
public class TestAverageDifferencesService
{
  [Test]
  public void GivenEmptyArray_WhenGetDifference_ThenEmptyList()
  {
    // Arrange
    AverageOffsetService service = new AverageOffsetService();
    List<KlinesItem> givenData = new List<KlinesItem>();
    List<KlinesItem> currentRange = new List<KlinesItem>();

    // Action
    List<(double difference, int index)> result = GetDifferences( givenData, currentRange );

    // Assert
    result.Should().BeEmpty();
  }

  [Test]
  public void GivenRangeWithOneItemAndSameData_WhenDifference_ThenAllZeroResult()
  {
    // Arrange
    var givenData = new List<KlinesItem>
    {
      new () {High = 1, Low = 1},
      new () {High = 1, Low = 1},
      new () {High = 1, Low = 1},
      new () {High = 1, Low = 1},
    };

    var currentRange = new List<KlinesItem>()
    {
      new () {Open = 1, High = 1, Low = 1, Close = 1},
    };


    // Action
    List<(double difference, int index)> result = GetDifferences( givenData, currentRange );

    // Assert
    result.Should().BeEquivalentTo( new List<(double difference, int index)> { (0, 0), (0, 1), (0, 2), (0, 3)} );
  }

  [Test]
  public void GivenRangeWithOneItem_WhenGetDifference_ThenCorrectResult()
  {
    // Arrange
    var givenData = new List<KlinesItem>
    {
      new () {High = 1.2, Low = 1.1}, // average 1.15
      new () {High = 1.4, Low = 1.3}, // average 1.35
      new () {High = 1.6, Low = 1.5}, // average 1.55
      new () {High = 1.8, Low = 1.7}, // average 1.75
    };

    var currentRange = new List<KlinesItem>()
    {
      new () {High = 2, Low = 1}, // average 1.5
    };

    var expectedValues = new List<(double difference, int index)>
    {
      ( 1.5 - 1.15, 0),
      ( 1.5 - 1.35, 1),
      ( 1.5 - 1.55, 2),
      ( 1.5 - 1.75, 3)
    };

    // Action
    var result = GetDifferences( givenData, currentRange );

    // Assert
    result.Should().BeEquivalentTo( expectedValues );
  }

  [Test]
  public void GivenRangeWithTwoItems_WhenMatch_ThenCorrectResult()
  {
    // Arrange
    var givenData = new List<KlinesItem>
    {
      new () {High = 1.2, Low = 1.1}, // average 1.15
      new () {High = 1.4, Low = 1.3}, // average 1.35
      new () {High = 1.6, Low = 1.5}, // average 1.55
      new () {High = 1.8, Low = 1.7}, // average 1.75
    };

    var currentRange = new List<KlinesItem>()
    {
      new () {High = 1.5, Low = 1}, // average 1.25
      new () {High = 2, Low = 1.5}, // average 1.75
    };

    var expectedValues = new List<(double difference, int index)>
    {
      ( ((1.25 - 1.15) + (1.75 - 1.35)) / 2.0, 0),
      ( ((1.25 - 1.35) + (1.75 - 1.55)) / 2.0, 1),
      ( ((1.25 - 1.55) + (1.75 - 1.75)) / 2.0, 2)
    };

    // Action
    var result = GetDifferences( givenData, currentRange );

    // Assert
    result.Should().BeEquivalentTo( expectedValues );
  }

  private List<(double difference, int index)> GetDifferences( IList<KlinesItem> givenData, IList<KlinesItem> currentRange )
  {
    if ( !givenData.Any() )
    {
      return new List<(double difference, int index)>();
    }

    var service = new AverageOffsetService();

    var result = new List<(double difference, int index)>( givenData.Count );
    for ( var historyIndex = 0; historyIndex <= givenData.Count - currentRange.Count; historyIndex++ )
    {
      var difference = service.GetOffset( givenData, currentRange, historyIndex );
      result.Add( new(difference, historyIndex) );
    }

    return result;
  }
}