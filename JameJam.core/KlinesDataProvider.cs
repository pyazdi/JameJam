using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace JameJam.Binance.Core;

public class KlinesDataProvider
{
  public List<KlinesItem> Container { get; } = new List<KlinesItem>(2000);

  public int SetData( string[] givenData )
  {
    var numberOfImports = 0;
    foreach ( var line in givenData )
    {
      var fields = line.Split( ',' );
      if ( fields.Length != 12 )
      {
        // error
      }

      Container.Add( GetKlines( fields ) );
      numberOfImports++;
    }

    return Container.Count;
  }

  private KlinesItem GetKlines( string[] fields )
  {
    var result = new KlinesItem();
    double data;

    double.TryParse(fields[0], out data);
    result.OpenTime = TimeStampToUtcDateTime(data);

    double.TryParse(fields[1], out data);
    result.Open = data;

    double.TryParse(fields[2], out data);
    result.High = data;

    double.TryParse(fields[3], out data);
    result.Low = data;

    double.TryParse(fields[4], out data);
    result.Close = data;

    double.TryParse(fields[5], out data);
    result.Volume = data;

    double.TryParse(fields[6], out data);
    result.CloseTime = TimeStampToUtcDateTime(data);

    double.TryParse(fields[7], out data);
    result.Quote = data;

    double.TryParse(fields[8], out data);
    result.AssetVolume = data;

    double.TryParse(fields[9], out data);
    result.NumberOfTrades = data;

    double.TryParse(fields[10], out data);
    result.TakerBuyBaseAssetVolume = data;

    double.TryParse(fields[11], out data);
    result.TakerBuyQuoteAssetVolume = data;

    return result;
  }

  private DateTime TimeStampToUtcDateTime(double timeStamp)
  {
    // Binance timestamp is milliseconds past epoch
    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
    return epoch.AddMilliseconds(timeStamp);
  }

}