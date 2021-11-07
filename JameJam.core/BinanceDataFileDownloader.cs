using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.IO.Compression;
using Microsoft.Win32.SafeHandles;

namespace JameJam.Core
{
  public class BinanceDataFileDownloader
  {
    // Download folder for symbol data
    private string DownloadPath = @"./klines/";

    // Url building vars

    private BinanceDataSource DataSource = BinanceDataSource.Spot;
    private DataInterval DateRange = DataInterval.OneDay;
    private BinanceDataType DataType = BinanceDataType.Klines;


    // Download candle stick data from data.binance.vision
    public void DownloadKlines(  )
    {
    }

    //public List<List<byte>> DownloadZipFiles( string symbol, DataInterval interval )
    //{
    //  // Get current year/month/firstday of month
    //  var date = new DateTime( DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1 );
    //  var url = BuildUrl( symbol, interval );
    //  List<List<byte>> output = new List<List<byte>>();

    //  try
    //  {
    //    while ( true )
    //    {
    //      date = date.AddMonths( -1 );
    //      var file = BuildFile( symbol, interval, date );

    //      if ( !File.Exists( DownloadPath + file + ".csv" ) )
    //      {
    //        using ( var client = new WebClient() )
    //        {
    //          byte[] data = client.DownloadData( url + file + ".zip" );
    //          output.Add( data );
    //        }
    //      }
    //    }
    //  }
    //  catch ( Exception )
    //  {
    //    return output;
    //  }
    //}

    public void Extract(byte[] data )
    {
      Stream memoryStream = new MemoryStream(data); // The original data
      Stream unzippedEntryStream; // Unzipped data from a file in the archive

      var archive = new ZipArchive(memoryStream);
      foreach (var entry in archive.Entries)
      {
          unzippedEntryStream = entry. Open();
          using var outputFileStream = new FileStream( GetOutputPath( entry.Name ), FileMode.CreateNew );
          unzippedEntryStream.CopyTo(outputFileStream);
      }
    }

    private string GetOutputPath( string entryName )
    {
      var userFolderPath = Environment.GetFolderPath( Environment.SpecialFolder.UserProfile );
      var outputFolder = Path.Combine( userFolderPath, "BinanceData", entryName);
      return outputFolder;
    }
  }
}