using System.Collections.Generic;

namespace JameJam.Binance.Core
{
  public interface IMatchCalculatorService
  {
    double GetMatchFactor( IList<KlinesItem> historyData, IList<KlinesItem> currentRange, int historyIndex, double offset );
  }
}