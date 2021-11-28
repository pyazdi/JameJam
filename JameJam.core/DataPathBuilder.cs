using System;

namespace JameJam.Binance.Core
{
  public class DataPathBuilder
  {
    private string BaseUrl = @"https://data.binance.vision/data";
    private string DataPair ="BNBUSDT";

    public string GetPath (
      int year,
      int month,
      DataSource source,
      DataType dataType,
      DataInterval dataInterval )
    {
      // https://data.binance.vision/data/spot/monthly/klines/BNBUSDT/1m/BNBUSDT-1m-2018-01.zip

      return $"{BaseUrl}/{source.ToString().ToLower()}/monthly/{dataType.ToString().ToLower()}/{DataPair}/" +
             $"{GetKlineInterval(dataInterval)}/{DataPair}-{GetKlineInterval(dataInterval)}" +
             $"-{year}-{month:D2}.zip";
    }
    public string GetKlineInterval(DataInterval interval)
    {
      switch (interval)
      {
        case DataInterval.TwelveHour:
          return "12h";
        case DataInterval.FifteenMinute:
          return "15m";
        case DataInterval.OneDay:
          return "1d";
        case DataInterval.OneHour:
          return "1h";
        case DataInterval.OneMinute:
          return "1m";
        case DataInterval.OneMonth:
          return "1mo";
        case DataInterval.OneWeek:
          return "1w";
        case DataInterval.TwoHour:
          return "2h";
        case DataInterval.ThirtyMinute:
          return "30m";
        case DataInterval.ThreeDay:
          return "3d";
        case DataInterval.ThreeMinute:
          return "3m";
        case DataInterval.FourHour:
          return "4h";
        case DataInterval.FiveMinute:
          return "5m";
        case DataInterval.SixHour:
          return "6h";
        case DataInterval.EightHour:
          return "8h";
        default:
          throw new ArgumentException($"Invalid argument {interval}", "interval");
      }
    }

  }
}