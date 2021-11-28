using System;

namespace JameJam.Core;

public class BinanceDataConverter
{
  DateTime TimeStampToUtcDateTime(double timeStamp)
  {
    // Binance timestamp is milliseconds past epoch
    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
    return epoch.AddMilliseconds(timeStamp);
  }
}