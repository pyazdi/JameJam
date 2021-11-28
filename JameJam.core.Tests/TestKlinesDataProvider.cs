using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace JameJam.Binance.Core.Tests;

[TestFixture]
public class TestKlinesDataProvider
{
  [Test]
  public void GivenValidData_WhenSetData_ThenCorrectNumberOfData()
  {
    // Arrange
    var givenData = new []
    {
      "1625011200000,300.79000000,304.88000000,281.53000000,303.71000000,2061866.47000000,1625097599999,606540123.12940300,734913,1027784.82270000,302523256.29888100,0",
      "1625097600000,303.75000000,304.00000000,281.00000000,287.43000000,1388151.02650000,1625183999999,401830865.47626700,573928,688603.50030000,199336774.04715800,0"
    };

    var dataProvider = new KlinesDataProvider();

    // Action
    int numberOfImportedDataLines = dataProvider.SetData( givenData );

    // Assert
    numberOfImportedDataLines.Should().Be( 2 );
  }

  [Test]
  public void GivenValidData_WhenSetData_ThenCorrectFields()
  {
    // Arrange
    var givenData = new []
    {
      "1512086400000,1.95000000,2.13850000,1.88010000,2.05270000,321456.28000000,1512172799999,650183.35144600,2307       ,203762.85000000,413887.71514500        , 104723793.67154244"
    };

    var dataProvider = new KlinesDataProvider();

    // Action
    int numberOfImportedDataLines = dataProvider.SetData( givenData );

    // Assert
    dataProvider.Container.Should().HaveCount( 1 );
    var actualData = dataProvider.Container.First();
    actualData.OpenTime.Should().Be( new DateTime( 2017, 12, 1 ) );
    actualData.Open.Should().Be( 1.95000000 );
    actualData.High.Should().Be( 2.13850000 );
    actualData.Low.Should().Be( 1.88010000 );
    actualData.Close.Should().Be( 2.05270000 );
    actualData.Volume.Should().Be( 321456.28000000 );
    actualData.CloseTime.Should().Be( new DateTime( 2017, 12, 1,23,59,59,999 ) );
    actualData.Quote.Should().Be( 650183.35144600 );
    actualData.AssetVolume.Should().Be( 2307 );
    actualData.NumberOfTrades.Should().Be( 203762.85000000 );
    actualData.TakerBuyBaseAssetVolume.Should().Be( 413887.71514500 );
    actualData.TakerBuyQuoteAssetVolume.Should().Be( 104723793.67154244 );
  }
}