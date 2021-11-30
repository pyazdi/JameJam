using System.Collections.Generic;

namespace JameJam.Binance.Core
{
  public interface IAverageOffsetService
  {
    double GetOffset(IList<KlinesItem> givenData, IList<KlinesItem> currentRange, int historyIndex);
  }
}