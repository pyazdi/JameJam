using System;

namespace JameJam.Binance.Core
{
  // OpenTime     ,Open       ,High     ,Low       ,Close     ,Volume         ,CloseTime    ,Quote          ,AssetVolume,NumberOfTrades ,TakerBuyBaseAssetVolume, TakerBuyQuoteAssetVolume
  // 1512086400000,1.95000000,2.13850000,1.88010000,2.05270000,321456.28000000,1512172799999,650183.35144600,2307       ,203762.85000000,413887.71514500        , 104723793.67154244

  public class KlinesItem
  {
    public DateTime OpenTime { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }
    public double Volume { get; set; }
    public DateTime CloseTime { get; set; }
    public double Quote { get; set; }
    public double AssetVolume { get; set; }
    public double NumberOfTrades { get; set; }
    public double TakerBuyBaseAssetVolume { get; set; }
    public double TakerBuyQuoteAssetVolume { get; set; }
  };
}