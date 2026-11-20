using MegaCrit.Sts2.Core.Logging;
using System;

namespace CalculatorMod;


public static class MLog
{

    private static string Process(string msg)
    {
        return $"{Constants.LOG_PREFIX}{msg}";
    }

    public static void Info(string msg) {
        Log.Info(Process(msg));
    }

    public static void Warn(string msg) {
        Log.Warn(Process(msg));
    }

    public static void Error(string msg) {
        Log.Error(Process(msg));
    }

}