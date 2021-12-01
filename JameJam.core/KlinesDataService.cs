using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace JameJam.Binance.Core;

public class KlinesDataService
{
  public IList<KlinesItem> GetKlines( string[] givenData )
  {
    List<KlinesItem> container = new List<KlinesItem>(givenData.Length);
    for ( var lineNumber = 0; lineNumber < givenData.Length; lineNumber++ )
    {
      var line = givenData[lineNumber];
      var fields = line.Split( ',' );
      if ( fields.Length != 12 )
      {
        throw new InvalidDataException( $"Too few columns at line {lineNumber}" );
      }

      container.Add( GetKline( fields ) );
    }

    return container;
  }

  private KlinesItem GetKline( string[] fields )
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