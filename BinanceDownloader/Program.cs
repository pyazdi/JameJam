// See https://aka.ms/new-console-template for more information

using System.IO.Compression;
using System.Net;
using JameJam.Binance.Core;

var binanceDataPathBuilder = new DataPathBuilder();

var startDate = new DateTime( 2017, 12, 1 );
var lastDate = new DateTime( 2021, 10, 28 );
var outputFileName = $"daily-{startDate.Year}-{startDate.Month}--{lastDate.Year}-{lastDate.Month}.csv";

var outputPath = GetOutputPath( outputFileName );
Directory.CreateDirectory( Path.GetPathRoot( outputPath ));
using ( var outputFileStream = new FileStream( outputPath, FileMode.Create ) )

using ( var client = new WebClient() )
{
  var currentDate = startDate;
  while ( currentDate <= lastDate )
  {
    try
    {
      var actualPath = binanceDataPathBuilder.GetPath( currentDate.Year, currentDate.Month, DataSource.Spot, DataType.Klines, DataInterval.OneDay );
      Console.WriteLine( $"Getting file {actualPath}" );

      byte[] data = client.DownloadData( actualPath );
      Stream memoryStream = new MemoryStream( data ); // The original data
      var archive = new ZipArchive( memoryStream );
      foreach ( var entry in archive.Entries )
      {
        var unzippedEntryStream = entry.Open(); // Unzipped data from a file in the archive
        unzippedEntryStream.CopyTo( outputFileStream );
      }
    }
    catch ( Exception exception )
    {
      Console.WriteLine($"Error getting file. {exception}");
    }

    currentDate = currentDate.AddMonths( 1 );
  }
}

void AppendToFile ( Stream data, string filePathName )
{
  using var outputFileStream = new FileStream( filePathName, FileMode.Append );
  data.CopyTo( outputFileStream );
}

string GetOutputPath( string entryName )
{
  var userFolderPath = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments);
  var outputFolder = Path.Combine( userFolderPath,"JameJam", entryName );
  return outputFolder;
}