using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace JameJam.Binance.Core.Tests;

[TestFixture]
public class TestMatchDataService
{
  [Test]
  public void GivenEmptyData_WhenGetMatches_ThenEmptyList()
  {
    // Arrange
    var averageOffsetServiceMock = Substitute.For<IAverageOffsetService>();
    // offset is always zero
    averageOffsetServiceMock.GetOffset( Arg.Any<IList<KlinesItem>>(), Arg.Any<IList<KlinesItem>>(), Arg.Any<int>() ).Returns( 0 );

    var matchCalculatorServiceMock = Substitute.For<IMatchCalculatorService>();
    matchCalculatorServiceMock
      .GetMatchFactor( Arg.Any<IList<KlinesItem>>(), Arg.Any<IList<KlinesItem>>(), Arg.Any<int>(), Arg.Any<double>() )
      .Returns( x =>
      {
        var data = (IList<KlinesItem>) x[0];
        var index = (int) x[2];
        return data[index].High;
      } );

    var serviceUnderTest = new MatchDataService( averageOffsetServiceMock, matchCalculatorServiceMock );
    
    var givenData = new List<KlinesItem>
    {
      new () { Low  = 1, High = 2},
      new () { Low  = 3, High = 4},
      new () { Low  = 5, High = 6},
      new () { Low  = 7, High = 8},
      new () { Low  = 9, High = 10},
      new () { Low = 11, High = 12},
    };

    var currentRange = new List<KlinesItem>()
    {
      new () { Low = 20, High = 21},
      new () { Low = 22, High = 23},
      new () { Low = 24, High = 25},
    };

    var expectedResult = new List<(double factor, int index)>()
    {
      new ( 2, 0),
      new ( 4, 1),
      new ( 6, 2),
      new ( 8, 3),
    };
    // Action
    var matches = serviceUnderTest.GetMatchFactor( givenData, currentRange );

    // Assert
    matches.Should().BeEquivalentTo( expectedResult );
  }
}