﻿using System.Configuration;
using System.Globalization;

namespace MVCClient.Configuration
{
    public static class SettingsManager
    {
        public static string BaseServiceUrl
        {
            get { return ConfigurationManager.AppSettings["BaseServiceUrl"]; }
        }

        public static int AutoCompleteMinLenght = 4;
        public static string DateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        public static string DateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
        public static string NumberFormat = "{0:n0}";
        public static string YearMonthPattern = CultureInfo.CurrentCulture.DateTimeFormat.YearMonthPattern;
        public static int GridPopupHeight = 263;
        public static int GridPopupNoTabHeight = 330;

        public static string MonthDayPattern
        {
            get
            {
                string shortDatePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                while (shortDatePattern[0] != 'd' && shortDatePattern[0] != 'M')
                {
                    shortDatePattern = shortDatePattern.Substring(1);
                    if (shortDatePattern.Length == 0)
                        return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                }
                while (shortDatePattern[shortDatePattern.Length - 1] != 'd' && shortDatePattern[shortDatePattern.Length - 1] != 'M')
                {
                    shortDatePattern = shortDatePattern.Substring(0, shortDatePattern.Length - 1);
                    if (shortDatePattern.Length == 0)
                        return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                }
                return shortDatePattern;
            }
        }
    }

    public class MySettingsManager
    {
        public string BaseServiceUrl { get { return SettingsManager.BaseServiceUrl; } }

        public int AutoCompleteMinLenght { get { return SettingsManager.AutoCompleteMinLenght; } }
        public string DateFormat { get { return SettingsManager.DateFormat; } }
        public string DateTimeFormat { get { return SettingsManager.DateTimeFormat; } }
        public string NumberFormat { get { return SettingsManager.NumberFormat; } }
        public string YearMonthPattern { get { return SettingsManager.YearMonthPattern; } }
        public string MonthDayPattern { get { return SettingsManager.MonthDayPattern; } }
        public int GridPopupHeight { get { return SettingsManager.GridPopupHeight; } }
        public int GridPopupNoTabHeight { get { return SettingsManager.GridPopupNoTabHeight; } }
    }

}