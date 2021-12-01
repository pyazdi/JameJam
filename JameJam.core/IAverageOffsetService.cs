using System.Collections.Generic;

namespace JameJam.Binance.Core
{
  public interface IAverageOffsetService
  {
    double GetOffset(IList<KlinesItem> historyData, IList<KlinesItem> currentRange, int historyIndex);
  }
}