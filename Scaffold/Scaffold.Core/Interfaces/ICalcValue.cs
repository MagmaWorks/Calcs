﻿using Scaffold.Core.Enums;

namespace Scaffold.Core.Interfaces;

public interface ICalcValue
{
    IoDirection Direction { get; }
    string DisplayName { get; }
    string Symbol { get; }
    CalcStatus Status { get; set; }    
    void SetValue(string strValue);
    string GetValue(string format = "");
    string ToString();
}