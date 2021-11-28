using FluentAssertions;
using JameJam.Binance.Core;
using NUnit.Framework;

namespace JameJam.Binance.Core.Tests
{
  [TestFixture()]
  public class TestBinanceDataPathBuilder
  {
    [Test]
    public void GetPathTest()
    {
      var service = new DataPathBuilder();
      
      var actualPath = service.GetPath( 2018, 1, DataSource.Spot, DataType.Klines, DataInterval.OneMinute);
      actualPath.Should().Be( "https://data.binance.vision/data/spot/monthly/klines/BNBUSDT/1m/BNBUSDT-1m-2018-01.zip" );
    }

    [Test]
    public void GivenGIVEN_WhenMETHOD_ThenTHEN()
    {


      // Action

      // Assert
    }
  }
}