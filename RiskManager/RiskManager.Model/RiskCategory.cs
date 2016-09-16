using System;

namespace RiskManager.Model
{
    [Flags]
    public enum RiskCategory
    {
        None,
        UnusualWinRate = 1,
        TenTimesAverage = 2,
        ThirtyTimesAverage = 4,
        ThousandDollarWin = 8
    }
}
