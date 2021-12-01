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
        return data[index].Close;
      } );

    var serviceUnderTest = new MatchDataService( averageOffsetServiceMock, matchCalculatorServiceMock );
    
    var givenData = new List<KlinesItem>
    {
      new () { Open  = 1, Close = 2},
      new () { Open  = 3, Close = 4},
      new () { Open  = 5, Close = 6},
      new () { Open  = 7, Close = 8},
      new () { Open  = 9, Close = 10},
      new () { Open = 11, Close = 12},
    };

    var currentRange = new List<KlinesItem>()
    {
      new () { Open = 20, Close = 21},
      new () { Open = 22, Close = 23},
      new () { Open = 24, Close = 25},
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