using NUnit.Framework;
using JameJam.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using FluentAssertions;

namespace JameJam.Core.Tests
{
  [TestFixture()]
  public class TestBinanceDataPathBuilder
  {
    [Test]
    public void GetPathTest()
    {
      var service = new BinanceDataPathBuilder();
      
      var actualPath = service.GetPath( 2018, 1, BinanceDataSource.Spot, BinanceDataType.Klines, DataInterval.OneMinute);
      actualPath.Should().Be( "https://data.binance.vision/data/spot/monthly/klines/BNBUSDT/1m/BNBUSDT-1m-2018-01.zip" );
    }

    [Test]
    public void GivenGIVEN_WhenMETHOD_ThenTHEN()
    {
      var service = new BinanceDataPathBuilder();
      var actualPath = service.GetPath(2017, 12, BinanceDataSource.Spot, BinanceDataType.Klines, DataInterval.OneMinute);

      BinanceDataFileDownloader downloader = new BinanceDataFileDownloader();
      // Arrange
      using (var client = new WebClient())
      {
        byte[] data = client.DownloadData(actualPath);
        downloader.Extract( data );
      }


      // Action

      // Assert
    }
  }
}